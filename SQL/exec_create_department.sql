-- 測試插入部門並自動生成 DEPT_NO
DECLARE
  v_department t_department := t_department(NULL, 'HR', 'New York');
BEGIN
  pkg_departments.create_department(v_department);
  DBMS_OUTPUT.PUT_LINE('Created Department: Dept No = ' || v_department.dept_no || ', Name = ' || v_department.name || ', Location = ' || v_department.location);
END;
