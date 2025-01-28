-- 測試以部門編號查詢
DECLARE
  v_cursor SYS_REFCURSOR;
  v_dept_no NUMBER;
  v_name VARCHAR2(50);
  v_location VARCHAR2(50);
BEGIN
  v_cursor := pkg_departments.get_department(1); -- 查詢 DEPT_NO = 1
  LOOP
    FETCH v_cursor INTO v_dept_no, v_name, v_location;
    EXIT WHEN v_cursor%NOTFOUND;
    DBMS_OUTPUT.PUT_LINE('Dept No: ' || v_dept_no || ', Name: ' || v_name || ', Location: ' || v_location);
  END LOOP;
  CLOSE v_cursor;
END;
