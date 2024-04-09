using System.Globalization;
using UnityEngine;
using System.IO;


public static class Dzhake
{
    public static void Log(string message)
    {
        Debug.Log("[Dzhake] " + message);
    }

    public static readonly string ModConfigPath = "Dzhakes mod/config.txt";
    public static readonly string Version = "Dzhakes mod v3";

    public static float EnemyHpMod = 1f;
    public static float EnemyBulletSpeedMod = 1f;
    public static float EnemyPatternSpeedMod = 1f;
    public static float EnemyMovingSpeedMod = 1f;

    public static bool PlayerHasOneHp = false;
    public static bool IronMode = false;
    public static bool SkipIntro = false;
    public static bool PracticeMode = false;
    public static bool DisableHUD = false;


    public static float PlayerFireRangeMul = 1f;
    public static float PlayerDamageMul = 1f;
    public static float PlayerFireRateMul = 1f;
    public static float PlayerBoostSpeedMul = 1f;
    public static float PlayerMoveSpeedMul = 1f;
    public static float PlayerBulletSpeedMul = 1f;
    public static float PlayerCriticChanceMul = 1f;

    public static float DodgeChanceMul = 1f;

    
    public static void Init()
    {
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

        Log("config.txt exists: " + File.Exists(ModConfigPath).ToString());
        if (File.Exists(ModConfigPath))
        {

            string[] lines = File.ReadAllLines(ModConfigPath);
            Log("lines.Length: " + lines.Length);

            foreach (string line in lines)
            {
                Log(line);
                string[] values = line.Split(':');
                string key = values[0];
                string value = values[1];
                switch (key)
                {
                    case "EnemyBulletSpeed":
                        EnemyBulletSpeedMod = float.Parse(value); break;
                    case "EnemyPatternSpeed":
                        EnemyPatternSpeedMod = float.Parse(value); break;
                    case "EnemyMovingSpeed":
                        EnemyMovingSpeedMod = float.Parse(value); break;
                    case "EnemyHp":
                        EnemyHpMod = float.Parse(value); break;
                    case "PlayerHasOneHp":
                        PlayerHasOneHp = bool.Parse(value); break;
                    case "GameSpeed":
                        SGTime.GameSpeedScale = float.Parse(value);
                        GameSettings.GameSpeed = float.Parse(value);
                        break;

                    case "StartingItems":
                        string[] items = value.Split(',');
                        foreach (string item in items)
                        {
                            switch (item.ToLower())
                            {
                                /*case "Gun":
                                    PlayerState.StatsLevel[Stats.BulletNumber] = Math.Max(PlayerState.StatsLevel[Stats.BulletNumber], 1);
                                    break;*/
                                case "supershot":
                                    PlayerState.SetSkill(Skill.Supershot, unlocked: true);
                                    break;
                                case "boost":
                                    PlayerState.SetSkill(Skill.Boost, unlocked: true);
                                    Player.Control.UpdateBoostHoldTime(Skill.Dash);
                                    break;
                                case "dash":
                                    PlayerState.SetSkill(Skill.Dash, unlocked: true);
                                    Player.Control.UpdateBoostHoldTime(Skill.Dash);
                                    break;
                                case "hover":
                                    PlayerState.SetSkill(Skill.Hover, unlocked: true);
                                    break;

                                case "1;1":
                                case "primodial crystal":
                                    PlayerState.Modules[Modules.PrimordialCrystal] = true; break;
                                case "1;2":
                                case "idol of protection":
                                    PlayerState.Modules[Modules.IdolBomb] = true; break;
                                case "2;2":
                                case "idol of time":
                                    PlayerState.Modules[Modules.IdolSlow] = true; break;
                                case "3;2":
                                case "idol of spirits":
                                    PlayerState.Modules[Modules.IdolAlly] = true; break;
                                case "4;2":
                                case "overcharge":
                                    PlayerState.Modules[Modules.Overcharge] = true; break;
                                case "1;3":
                                case "compass":
                                    PlayerState.Modules[Modules.Compass] = true; break;
                                case "2;3":
                                case "village star":
                                    PlayerState.Modules[Modules.Teleport] = true; break;
                                case "3;3":
                                case "ancient astrolabe":
                                    PlayerState.Modules[Modules.CollectableScan] = true; break;
                                case "4;3":
                                case "crystal bullet":
                                    PlayerState.Modules[Modules.BlueBullet] = true; break;
                                case "1;4":
                                case "lucky heart":
                                    PlayerState.Modules[Modules.HpDrop] = true; break;
                                case "2;4":
                                case "enchanted heart":
                                    PlayerState.Modules[Modules.HearthCrystal] = true; break;
                                case "3;4":
                                case "enchanted powers":
                                    PlayerState.Modules[Modules.FreePower] = true; break;
                                case "4;4":
                                case "advanced energy":
                                    PlayerState.Modules[Modules.BoostCost] = true; break;
                                case "1;5":
                                case "vengeful talisman":
                                    PlayerState.Modules[Modules.Retaliation] = true; break;
                                case "2;5":
                                case "wounded heart":
                                    PlayerState.Modules[Modules.Rage] = true; break;
                                case "3;5":
                                case "restoration enchancer":
                                    PlayerState.Modules[Modules.XpGain] = true; break;
                                case "4;5":
                                case "spirit dash":
                                    PlayerState.Modules[Modules.SpiritDash] = true; break;
                            }
                        }
                        break;

                    case "PlayerStat":
                        float value2 = float.Parse(values[2]);
                        switch (value.ToLower())
                        {
                            case "firerange":
                                PlayerFireRangeMul = value2; break;
                            case "damage":
                                PlayerDamageMul = value2; break;
                            case "firerate":
                                PlayerFireRateMul = value2; break;
                            case "boostspeed":
                                PlayerBoostSpeedMul = value2; break;
                            case "movespeed":
                                PlayerMoveSpeedMul = value2; break;
                            case "bulletspeed":
                                PlayerBulletSpeedMul = value2; break;
                            case "criticchance":
                                PlayerCriticChanceMul = value2; break;

                            case "dodgechance":
                                DodgeChanceMul = value2; break;
                        }
                        Player.Weapon.UpdateStats();
                        break;
                    case "IronMode":
                        IronMode = bool.Parse(value); break;
                    case "SkipIntro":
                        SkipIntro = bool.Parse(value); break;
                    case "PracticeMode":
                        PracticeMode = bool.Parse(value); break;
                    case "DisableHUD":
                        DisableHUD = bool.Parse(value);
                        UIManager.HUD.Deactivate();
                        break;
                }
            }
        }
    }
}