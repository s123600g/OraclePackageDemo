# OraclePackageDemo

環境資訊：
1. 技術框架：.Net 8
2. 資料庫：Oracle Database Free
3. 資料庫套件：
    - Dapper
    - Oracle.ManagedDataAccess.Core (ODP.NET)

程式專案：
1. OracleDbLib -> 資料庫Repository
   - 位置：`src/Db/OracleDbLib`。
3. OraclePackageSampleApp -> Console應用程式
   - 位置：`src/OraclePackageDemoApp`。

# 專案內容簡介

## OracleDbLib 資料庫Repository

1. 資料庫有端邏輯操作(基本CRUD) -> `src/Db/OracleDbLib/Repository/DepartmentRepository.cs`
2. 映射Package的Type參數 -> `src/Db/OracleDbLib/Entity/Udt/DepartmentUdt.cs`，可以對照`SQL/package_department.sql`裡面以下建立Type的語法內欄位
  ```
  CREATE OR REPLACE TYPE t_department AS OBJECT (
      dept_no    NUMBER,
      name       VARCHAR2(50),
      location   VARCHAR2(50)
  );
  ```

## OraclePackageDemoApp -> Console應用程式

1. 程式開始進入點 -> `src/OraclePackageDemoApp/Program.cs`
   - 從try-catch區塊內，分別對應針對資料庫基本CRUD操作呼叫服務

# 前置作業

## 資料庫建立

以[Podman](https://podman.io/)容器使用為例

Oracle 官方容器庫說明：[Oracle Database Free - Repository](https://container-registry.oracle.com/ords/ocr/ba/database)


**Step 1. 建立掛載區**
```
podman volume create oracle_db
```

建立完可透過以下指令查看確認
```
podman volume ls
```

**Step 2. 建立Oracle資料庫容器服務**
```
podman run --name=oracle-db-free -p 1521:1521 -e ORACLE_PWD=PassW0rd -d -v oracle_db:/opt/oracle/oradata container-registry.oracle.com/database/free:latest
```
- `—name` 參數
  - 容器服務名稱。
- `-p`參數
  - 容器開放與本機對接Port設定。
  - 預設容器內部Export 1521 Port，本機使用本身1521 Port與其串接。
- `-e`參數
  - 設置容器內環境變數。
  - `ORACLE_PWD`
    - 設置資料庫特定使用者預設密碼，使用此參數設置後，其密碼會記錄在容器內部環境變數之中，
    - 範例預設密碼設置為`PassW0rd`
- `-v`參數
  - 容器服務指定位置設定掛載區。
- `-d`參數
  - 設置容器執行在背景運作(detached mode)，不卡住執行動作終端機。

## 資料庫設定 - 建立登入帳戶

這裡需要開啟終端機進行操作。

**Step 1. 登入資料庫，使用sysdba身份**

這裡是透過Podman提供`exec`指令直接通資料庫容器服務內下指令操作，資料庫連線指令透過`sqlplus`。

指令格式參考
```
podman exec -it <容器服務名稱> sqlplus sys/<資料庫密碼>@FREE as sysdba
```

實際執行指令
```
podman exec -it oracle-db-free sqlplus sys/PassW0rd@FREE as sysdba
```

**Step 2. 切換SESSION至指定PDB**

free版本預設底下有兩個PDB
1. `PDB$SEED`
2. `FREEPDB1` <-- 都是在此PDB底下做設定操作

指令格式參考
```
ALTER SESSION SET CONTAINER = <PDB 名稱>;
```

實際執行指令
```
ALTER SESSION SET CONTAINER = FREEPDB1;
```

**Step 3. 建立使用者**

指令格式參考
```
CREATE USER <使用者名稱> IDENTIFIED BY <使用者密碼>;
```

實際執行指令
```
CREATE USER demo IDENTIFIED BY PassW0rd;
```
- 這裡建立demo使用者，呼應專案內部連線字串設定內登入使用者。

**Step 4. 設定使用者權限**

這裡設置都先將權限全開，正式環境不會這樣搞。

```
GRANT ALL PRIVILEGES TO test01;
```

## 資料庫設定 - 建立資料表、新增範例資料、建立Package

透過資料庫連線管理工具(例如：[Oracle SQL Developer](https://www.oracle.com/database/sqldeveloper/))，登入所建立使用者，並將位在專案內`SQL/`底下SQL腳本檔案依據以下順序執行。

執行順序
1. create_table.sql
2. insert_data_departments.sql
3. package_department.sql
