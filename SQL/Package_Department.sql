-- Package specification for DEPARTMENTS CRUD operations
CREATE OR REPLACE PACKAGE pkg_departments IS
  -- Procedure to create a new department
  PROCEDURE create_department(p_department IN t_department);

  -- Function to read department details
  FUNCTION get_department(p_dept_no IN NUMBER DEFAULT NULL) RETURN SYS_REFCURSOR;

  -- Procedure to update department details
  PROCEDURE update_department(p_department IN t_department);

  -- Procedure to delete a department
  PROCEDURE delete_department(p_dept_no IN NUMBER);
END pkg_departments;
/

-- Package body for DEPARTMENTS CRUD operations
CREATE OR REPLACE PACKAGE BODY pkg_departments IS
  PROCEDURE create_department(p_department IN t_department) IS
  BEGIN
    INSERT INTO DEPARTMENTS (DEPT_NO, NAME, LOCATION)
    VALUES (p_department.dept_no, p_department.name, p_department.location);
  END create_department;

  FUNCTION get_department(p_dept_no IN NUMBER DEFAULT NULL) RETURN SYS_REFCURSOR IS
    v_cursor SYS_REFCURSOR;
  BEGIN
    IF p_dept_no IS NULL THEN
      OPEN v_cursor FOR
        SELECT * FROM DEPARTMENTS;
    ELSE
      OPEN v_cursor FOR
        SELECT * FROM DEPARTMENTS WHERE DEPT_NO = p_dept_no;
    END IF;
    RETURN v_cursor;
  END get_department;

  PROCEDURE update_department(p_department IN t_department) IS
  BEGIN
    UPDATE DEPARTMENTS
    SET NAME = p_department.name,
        LOCATION = p_department.location
    WHERE DEPT_NO = p_department.dept_no;
  END update_department;

  PROCEDURE delete_department(p_dept_no IN NUMBER) IS
  BEGIN
    DELETE FROM DEPARTMENTS WHERE DEPT_NO = p_dept_no;
  END delete_department;
END pkg_departments;
/