# 積算枚数レポート (CumulativeCountReport)

装置毎に日ごとの最終積算枚数を表示するASP.NET Core Razor Pagesアプリケーション

## 機能

- 装置毎に過去7日間の積算枚数を表示
- 工程(AREA)によるフィルター機能
- 装置のグループ化及び並び順(AREA, GROUP, ORDER, EOPIDの昇順)
- 管理値の80%以上の値を赤文字で強調表示

## 画面表示項目

- EOPID
- TESTOPNO
- ITEMPROMPT
- Control Value (管理値)
- 過去7日間の日付列 (最新日が左側)

## データベーステーブル

### Wafer_Count_History
- EOPID
- TESTOPNO
- DATE
- ITEMPROMPT
- VALUE

### Equipment
- EOPID
- AREA
- GROUP
- ORDER

### Doop_Control_Value
- EOPID
- TESTOPNO
- ITEMPROMPT
- VALUE

## セットアップ手順

### 1. データベース接続文字列の設定

`appsettings.json` ファイルの `ConnectionStrings` セクションを編集して、実際のデータベース接続情報を設定してください。

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YourServerName;Database=YourDatabaseName;User Id=YourUsername;Password=YourPassword;TrustServerCertificate=True;"
}
```

### 2. パッケージの復元

```bash
dotnet restore
```

### 3. アプリケーションのビルド

```bash
dotnet build
```

### 4. アプリケーションの実行

```bash
dotnet run
```

アプリケーションは `https://localhost:5001` または `http://localhost:5000` で起動します。

### 5. レポートページへのアクセス

ブラウザで以下のURLにアクセスしてください:
```
https://localhost:5001/CumulativeCountReport
```

## 使用方法

1. ページを開くと、過去7日間の積算枚数レポートが表示されます
2. 工程フィルターのドロップダウンから特定のAREAを選択すると、そのエリアのデータのみが表示されます
3. 管理値(Control Value)の80%以上の値は赤い太字で表示されます

## プロジェクト構造

```
CumulativeCountReport/
├── Data/
│   └── ApplicationDbContext.cs      # データベースコンテキスト
├── Models/
│   ├── WaferCountHistory.cs         # Wafer_Count_Historyテーブルモデル
│   ├── Equipment.cs                 # Equipmentテーブルモデル
│   ├── DoopControlValue.cs          # Doop_Control_Valueテーブルモデル
│   └── CumulativeCountReportViewModel.cs  # ビューモデル
├── Pages/
│   ├── CumulativeCountReport/
│   │   ├── Index.cshtml             # レポートビュー
│   │   └── Index.cshtml.cs          # レポートページモデル
│   ├── Shared/
│   │   └── _Layout.cshtml           # レイアウトページ
│   ├── _ViewImports.cshtml
│   └── _ViewStart.cshtml
├── wwwroot/
│   └── css/
├── appsettings.json                 # アプリケーション設定
├── appsettings.Development.json
├── Program.cs                       # アプリケーションエントリポイント
└── CumulativeCountReport.csproj     # プロジェクトファイル
```

## 技術スタック

- .NET 8.0
- ASP.NET Core Razor Pages
- Entity Framework Core 8.0
- SQL Server
- Bootstrap 5.3

## 注意事項

- データベースに既存のテーブル(Wafer_Count_History, Equipment, Doop_Control_Value)が存在する必要があります
- Entity Framework Coreのマイグレーション機能は使用していません(既存のデータベースを使用する前提)
- 日付は各日の最終値(最新のタイムスタンプ)を表示します
