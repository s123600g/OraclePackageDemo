-- 測試刪除部門
BEGIN
  pkg_departments.delete_department(4); -- 刪除 DEPT_NO = 4 的部門
  DBMS_OUTPUT.PUT_LINE('Department deleted successfully');
END;
/
