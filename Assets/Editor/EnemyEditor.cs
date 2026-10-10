using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyEditor : EditorWindow
{
    private const string ResourcesPath = "Assets/Resources/Enemies";

    private TextField nameField;
    private IntegerField hpField;
    private IntegerField damageField;
    private FloatField movementSpeedField;
    private FloatField attackSpeedField;
    private Toggle meleeField;
    private Toggle rangeField;
    private Toggle aoeField;
    private EnumField enemyTypeField;
    private Label statusLabel;

    [MenuItem("FarmSurvivor/Enemy Creator")]
    private static void Open()
    {
        var window = GetWindow<EnemyEditor>();
        window.titleContent = new GUIContent("Enemy Creator");
        window.minSize = new Vector2(320, 220);
    }

    private void CreateGUI()
    {
        var root = rootVisualElement;
        root.style.paddingLeft = root.style.paddingRight = 8;
        root.style.paddingTop = 8;

        root.Add(new Label("Create a new Enemy asset")
        {
            style = { unityFontStyleAndWeight = FontStyle.Bold, marginBottom = 6 }
        });

        nameField = new TextField("Enemy Name");
        enemyTypeField = new EnumField("Enemy Type", EnemyType.Zombie);
        hpField = new IntegerField("Hp");
        damageField = new IntegerField("Damage");
        movementSpeedField = new FloatField("Speed");
        attackSpeedField = new FloatField("Attack Speed");
        meleeField = new Toggle("Melee Attack");
        rangeField = new Toggle("Ranged Attack");
        aoeField = new Toggle("AOE Attack");

        var createButton = new Button(CreateAsset) { text = "Create" };
        createButton.style.marginTop = 8;

        statusLabel = new Label();
        statusLabel.style.marginTop = 6;
        statusLabel.style.whiteSpace = WhiteSpace.Normal;

        root.Add(nameField);
        root.Add(enemyTypeField);

        root.Add(new Label("Stats")
        {
            style = { marginBottom = 6, marginTop = 12 }
        });

        root.Add(hpField);
        root.Add(damageField);
        root.Add(attackSpeedField);
        root.Add(movementSpeedField);

        root.Add(new Label("Attacks")
        {
            style = { marginBottom = 6, marginTop = 12 }
        });

        root.Add(meleeField);
        root.Add(rangeField);
        root.Add(aoeField);
        root.Add(createButton);
        root.Add(statusLabel);
    }

    private void CreateAsset()
    {
        statusLabel.style.color = Color.red;
        if (string.IsNullOrWhiteSpace(nameField.value))
        {
            statusLabel.text = "Enemy name is required.";
            return;
        }
        if (hpField.value <= 0)
        {
            statusLabel.text = "Hp needs to be greater than 0.";
            return;
        }
        if (damageField.value <= 0)
        {
            statusLabel.text = "Damage needs to be greater than 0.";
            return;
        }
        if (attackSpeedField.value <= 0f)
        {
            statusLabel.text = "Attack speed needs to be greater than 0.";
            return;
        }
        if (movementSpeedField.value <= 0f)
        {
            statusLabel.text = "Movement speed needs to be greater than 0.";
            return;
        }

        if (meleeField.value == false && rangeField.value == false && aoeField.value == false)
        {
            statusLabel.text = "Must have at least one attack field.";
            return;
        }
        Directory.CreateDirectory(ResourcesPath);

        var enemy = ScriptableObject.CreateInstance<EnemyData>();
        enemy.hp = hpField.value;
        enemy.damage = damageField.value;
        enemy.attackSpeed = attackSpeedField.value;
        enemy.movementSpeed = movementSpeedField.value;
        enemy.enemyType = (EnemyType)enemyTypeField.value;

        string safeName = string.Join("_", nameField.value.Split(Path.GetInvalidFileNameChars()));
        string path = AssetDatabase.GenerateUniqueAssetPath($"{ResourcesPath}/{safeName}.asset");

        AssetDatabase.CreateAsset(enemy, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorGUIUtility.PingObject(enemy);
        Selection.activeObject = enemy;
        statusLabel.style.color = Color.green;
        statusLabel.text = $"Created {path}";
    }
}