-- 測試新增部門
DECLARE
  v_department t_department := t_department(4, 'Marketing', 'Boston');
BEGIN
  pkg_departments.create_department(v_department);
  DBMS_OUTPUT.PUT_LINE('Department created successfully');
END;
/
