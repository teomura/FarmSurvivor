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
    private IntegerField requiredLevel;
    private FloatField movementSpeedField;
    private FloatField attackSpeedField;
    private Toggle rangeField;
    private Toggle aoeField;
    private EnumField enemyTypeField;
    private Label statusLabel;
    private IntegerField hpDropChance;

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
        requiredLevel = new IntegerField("Min Player Level");
        hpField = new IntegerField("Hp");
        damageField = new IntegerField("Damage");
        movementSpeedField = new FloatField("Speed");
        attackSpeedField = new FloatField("Attack Speed");
        rangeField = new Toggle("Ranged Attack");
        aoeField = new Toggle("AOE Attack");
        hpDropChance = new IntegerField("Hp Drop Chance");

        var createButton = new Button(CreateAsset) { text = "Create" };
        createButton.style.marginTop = 8;

        statusLabel = new Label();
        statusLabel.style.marginTop = 6;
        statusLabel.style.whiteSpace = WhiteSpace.Normal;

        root.Add(nameField);
        root.Add(enemyTypeField);
        root.Add(requiredLevel);

        root.Add(new Label("Stats")
        {
            style = { marginBottom = 6, marginTop = 12 }
        });

        root.Add(hpField);
        root.Add(damageField);
        root.Add(attackSpeedField);
        root.Add(movementSpeedField);
        root.Add(hpDropChance);

        root.Add(new Label("Attacks")
        {
            style = { marginBottom = 6, marginTop = 12 }
        });

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
        if (requiredLevel.value <= 0)
        {
            statusLabel.text = "Level needs to be greater than 0.";
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
        if (hpDropChance.value < 0 || hpDropChance.value > 100)
        {
            statusLabel.text = "Drop chance must be between 0 and 100.";
            return;
        }
        Directory.CreateDirectory(ResourcesPath);

        var enemy = ScriptableObject.CreateInstance<EnemyData>();
        enemy.hp = hpField.value;
        enemy.damage = damageField.value;
        enemy.attackSpeed = attackSpeedField.value;
        enemy.movementSpeed = movementSpeedField.value;
        enemy.enemyType = (EnemyType)enemyTypeField.value;
        enemy.requiredLevel = requiredLevel.value;
        enemy.hpDropChance = hpDropChance.value;
        if (rangeField.value)
        {
            enemy.attacks.Add(AttackType.Ranged);
        }
        if (aoeField.value)
        {
            enemy.attacks.Add(AttackType.AOE);
        }

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