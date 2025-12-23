using System;

namespace CumulativeCountReport.Utilities
{
    public static class WaferCountSqlUtility
    {
        /// <summary>
        /// Areaに応じたWaferCount取得用のSQLクエリを返します
        /// </summary>
        /// <param name="area">エリア名（nullの場合はデフォルトのクエリを返す）</param>
        /// <returns>SQLクエリ文字列</returns>
        public static string GetWaferCountSqlByArea(string? area)
        {
            // Areaに応じて異なるSQLクエリを返す
            return area switch
            {
                "Area1" => GetArea1Sql(),
                "Area2" => GetArea2Sql(),
                "Area3" => GetArea3Sql(),
                _ => GetDefaultSql()
            };
        }

        /// <summary>
        /// デフォルトのSQLクエリ
        /// </summary>
        private static string GetDefaultSql()
        {
            return @"
                WITH RankedData AS (
                    SELECT
                        EopId,
                        TestOpNo,
                        ItemPrompt,
                        CAST(Date AS DATE) AS Date,
                        Value,
                        ROW_NUMBER() OVER (
                            PARTITION BY EopId, TestOpNo, ItemPrompt, CAST(Date AS DATE)
                            ORDER BY Date DESC
                        ) AS rn
                    FROM WaferCountHistories
                    WHERE CAST(Date AS DATE) BETWEEN {0} AND {1}
                )
                SELECT EopId, TestOpNo, ItemPrompt, Date, Value
                FROM RankedData
                WHERE rn = 1";
        }

        /// <summary>
        /// Area1用のSQLクエリ（仮実装）
        /// </summary>
        private static string GetArea1Sql()
        {
            return @"
                WITH RankedData AS (
                    SELECT
                        EopId,
                        TestOpNo,
                        ItemPrompt,
                        CAST(Date AS DATE) AS Date,
                        Value,
                        ROW_NUMBER() OVER (
                            PARTITION BY EopId, TestOpNo, ItemPrompt, CAST(Date AS DATE)
                            ORDER BY Date DESC
                        ) AS rn
                    FROM WaferCountHistories
                    WHERE CAST(Date AS DATE) BETWEEN {0} AND {1}
                )
                SELECT EopId, TestOpNo, ItemPrompt, Date, Value
                FROM RankedData
                WHERE rn = 1
                ORDER BY EopId, TestOpNo";
        }

        /// <summary>
        /// Area2用のSQLクエリ（仮実装）
        /// </summary>
        private static string GetArea2Sql()
        {
            return @"
                WITH RankedData AS (
                    SELECT
                        EopId,
                        TestOpNo,
                        ItemPrompt,
                        CAST(Date AS DATE) AS Date,
                        Value * 2 AS Value,
                        ROW_NUMBER() OVER (
                            PARTITION BY EopId, TestOpNo, ItemPrompt, CAST(Date AS DATE)
                            ORDER BY Date DESC
                        ) AS rn
                    FROM WaferCountHistories
                    WHERE CAST(Date AS DATE) BETWEEN {0} AND {1}
                )
                SELECT EopId, TestOpNo, ItemPrompt, Date, Value
                FROM RankedData
                WHERE rn = 1";
        }

        /// <summary>
        /// Area3用のSQLクエリ（仮実装）
        /// </summary>
        private static string GetArea3Sql()
        {
            return @"
                WITH RankedData AS (
                    SELECT
                        EopId,
                        TestOpNo,
                        ItemPrompt,
                        CAST(Date AS DATE) AS Date,
                        AVG(Value) AS Value,
                        ROW_NUMBER() OVER (
                            PARTITION BY EopId, TestOpNo, ItemPrompt, CAST(Date AS DATE)
                            ORDER BY MAX(Date) DESC
                        ) AS rn
                    FROM WaferCountHistories
                    WHERE CAST(Date AS DATE) BETWEEN {0} AND {1}
                    GROUP BY EopId, TestOpNo, ItemPrompt, CAST(Date AS DATE)
                )
                SELECT EopId, TestOpNo, ItemPrompt, Date, CAST(Value AS INT) AS Value
                FROM RankedData
                WHERE rn = 1";
        }
    }
}
