-- 測試刪除部門
BEGIN
  pkg_departments.delete_department(1); -- 刪除 DEPT_NO = 1 的部門
  DBMS_OUTPUT.PUT_LINE('Department deleted successfully.');
END;

