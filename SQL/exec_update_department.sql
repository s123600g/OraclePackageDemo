-- 測試更新部門
DECLARE
  v_department t_department := t_department(1, 'Human Resources', 'New York City');
BEGIN
  pkg_departments.update_department(v_department);
  DBMS_OUTPUT.PUT_LINE('Department updated successfully');
END;
/
