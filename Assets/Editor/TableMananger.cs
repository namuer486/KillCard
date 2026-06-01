using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Timeline;


public static class TableMananger
{
    public static string Cardspath = "Assets/TableDate/CardConfig.xlsx";
    public static string Playerpath = "Assets/TableDate/PlayerConfig.xlsx";
    public static string Enemypath = "Assets/TableDate/EnemyConfig.xlsx";
    [MenuItem("工具/导表 → 生成物品配置")]
    public static void LoadCardsExcel()
    {
        if (!File.Exists(Cardspath))
        {
            Debug.Log("文件不存在");
            return;
        }
        using (ExcelPackage package = new ExcelPackage(new FileInfo(Cardspath)))
        {
            if (package.Workbook.Worksheets.Count <= 0)
            {
                Debug.Log("没有工作表");
                return;
            }
            ExcelWorksheet worksheet = package.Workbook.Worksheets["CardConfig"];
            int maxRow = worksheet.Dimension.Rows;

            Resources.Load<CardsTable>("CardsTable").kards.Clear();
            for(int row = 3; row < maxRow + 1; row++)
            {
                int id = worksheet.Cells[row, 1].GetValue<int>();
                string name = worksheet.Cells[row, 2].GetValue<string>();
                string content = worksheet.Cells[row, 3].GetValue<string>();
                CardType type = (CardType)worksheet.Cells[row, 4].GetValue<int>();
                CardBuff buff = (CardBuff)worksheet.Cells[row, 5].GetValue<int>();
                float number = worksheet.Cells[row, 6].GetValue<float>();
                float buffnumber = worksheet.Cells[row, 7].GetValue<float>();
                int livenumber = worksheet.Cells[row, 8].GetValue<int>();
                string spritepath = worksheet.Cells[row, 9].GetValue<string>();
                //导出对应sprite
                CardConfig kard = new CardConfig
                {
                    id = id,
                    name = name,
                    content = content,
                    type = type,
                    buff = buff,
                    number = number,
                    buffnumber = buffnumber,
                    livenumber= livenumber
                    //sprite
                };
                Resources.Load<CardsTable>("CardsTable").kards.Add(kard);
            }
            EditorUtility.SetDirty(Resources.Load<CardsTable>("CardsTable"));
            Debug.Log("导入完成");
        }
    }
    [MenuItem("工具/导表 → 生成玩家配置")]
    public static void LoadPlayerExcel()
    {
        if (!File.Exists(Playerpath))
        {
            Debug.Log("文件不存在");
            return;
        }
        using (ExcelPackage package = new ExcelPackage(new FileInfo(Playerpath)))
        {
            if (package.Workbook.Worksheets.Count <= 0)
            {
                Debug.Log("没有工作表");
                return;
            }
            ExcelWorksheet worksheet = package.Workbook.Worksheets["PlayerConfig"];
            int maxRow = worksheet.Dimension.Rows;

            Resources.Load<PlayersTable>("PlayersTable").playerConfigs.Clear();
            for(int row = 2; row < maxRow + 1; row++)
            {

                int id= worksheet.Cells[row, 1].GetValue<int>();
                string name = worksheet.Cells[row, 2].GetValue<string>();
                int HP = worksheet.Cells[row, 3].GetValue<int>();
                int KardNumber = worksheet.Cells[row, 4].GetValue<int>();
                //导出对应sprite
                PlayerConfig player = new PlayerConfig
                {
                    HP = HP,
                    KardNumber = KardNumber,
                    id = id,
                    name = name,

                };
                Resources.Load<PlayersTable>("PlayersTable").playerConfigs.Add(player);
            }
            EditorUtility.SetDirty(Resources.Load<PlayersTable>("PlayersTable"));
            Debug.Log("导入完成");
        }
    }
    [MenuItem("工具/导表 → 生成怪物配置")]
    public static void LoadEnemyExcel()
    {
        if (!File.Exists(Playerpath))
        {
            Debug.Log("文件不存在");
            return;
        }
        using (ExcelPackage package = new ExcelPackage(new FileInfo(Enemypath)))
        {
            if (package.Workbook.Worksheets.Count <= 0)
            {
                Debug.Log("没有工作表");
                return;
            }
            ExcelWorksheet worksheet = package.Workbook.Worksheets["EnemyConfig"];
            int maxRow = worksheet.Dimension.Rows;

            Resources.Load<EnemyTable>("EnemyTable").enemies.Clear();
            for(int row = 2; row < maxRow + 1; row++)
            {

                int id= worksheet.Cells[row, 1].GetValue<int>();
                string name = worksheet.Cells[row, 2].GetValue<string>();
                int HP = worksheet.Cells[row, 3].GetValue<int>();
                EnemyType type = (EnemyType)worksheet.Cells[row, 4].GetValue<int>();
                //导出对应sprite
                EnemyConfig enemy = new EnemyConfig
                {
                    HP = HP,
                    name = name,
                    id = id,
                    type=type

                };
                Resources.Load<EnemyTable>("EnemyTable").enemies.Add(enemy);
            }
            EditorUtility.SetDirty(Resources.Load<EnemyTable>("EnemyTable"));
            Debug.Log("导入完成");
        }
    }
}

