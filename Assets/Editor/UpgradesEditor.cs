using System.IO;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class UpgradesEditor : EditorWindow
{
    private const string ResourcesPath = "Assets/Resources/Upgrades";

    private TextField nameField;
    private TextField descriptionField;
    private ObjectField spriteField;
    private IntegerField modifierField;
    private EnumField upgradeTypeField;
    private Label statusLabel;

    [MenuItem("FarmSurvivor/Upgrade Creator")]
    private static void Open()
    {
        var window = GetWindow<UpgradesEditor>();
        window.titleContent = new GUIContent("Upgrade Creator");
        window.minSize = new Vector2(320, 220);
    }

    private void CreateGUI()
    {
        var root = rootVisualElement;
        root.style.paddingLeft = root.style.paddingRight = 8;
        root.style.paddingTop = 8;

        root.Add(new Label("Create a new Upgrade asset")
        {
            style = { unityFontStyleAndWeight = FontStyle.Bold, marginBottom = 6 }
        });

        nameField = new TextField("Name");
        descriptionField = new TextField("Description");
        spriteField = new ObjectField("Image");
        spriteField.objectType = typeof(Sprite);
        modifierField = new IntegerField("Modifier");
        upgradeTypeField = new EnumField("Type", UpgradeType.HP);

        var createButton = new Button(CreateAsset) { text = "Create" };
        createButton.style.marginTop = 8;

        statusLabel = new Label();
        statusLabel.style.marginTop = 6;
        statusLabel.style.whiteSpace = WhiteSpace.Normal;

        root.Add(nameField);
        root.Add(upgradeTypeField);
        root.Add(spriteField);
        root.Add(descriptionField);
        root.Add(modifierField);

        root.Add(createButton);
        root.Add(statusLabel);
    }

    private void CreateAsset()
    {
        statusLabel.style.color = Color.red;
        if (string.IsNullOrWhiteSpace(nameField.value))
        {
            statusLabel.text = "Upgrade name is required.";
            return;
        }
        if (string.IsNullOrWhiteSpace(descriptionField.value))
        {
            statusLabel.text = "Description is required.";
            return;
        }
        if (modifierField.value <= 0)
        {
            statusLabel.text = "Modifier needs to be greater than 0.";
            return;
        }

        Directory.CreateDirectory(ResourcesPath);

        var upgrade = ScriptableObject.CreateInstance<Upgrade>();
        upgrade.name = nameField.value;
        upgrade.upgradeType = (UpgradeType)upgradeTypeField.value;
        upgrade.image = (Sprite)spriteField.value;
        upgrade.description = descriptionField.value;
        upgrade.modifier = modifierField.value;

        string safeName = string.Join("_", nameField.value.Split(Path.GetInvalidFileNameChars()));
        string path = AssetDatabase.GenerateUniqueAssetPath($"{ResourcesPath}/{safeName}.asset");

        AssetDatabase.CreateAsset(upgrade, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorGUIUtility.PingObject(upgrade);
        Selection.activeObject = upgrade;
        statusLabel.style.color = Color.green;
        statusLabel.text = $"Created {path}";
    }
}