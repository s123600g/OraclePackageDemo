-- Creating a type for DEPARTMENTS table
CREATE OR REPLACE TYPE t_department AS OBJECT (
  dept_no    NUMBER,
  name       VARCHAR2(50),
  location   VARCHAR2(50)
);
/

-- Package specification for DEPARTMENTS CRUD operations
CREATE OR REPLACE PACKAGE pkg_departments IS
  -- Procedure to create a new department
  PROCEDURE create_department(p_department IN OUT t_department);

  -- Function to read department details
  FUNCTION get_department(p_dept_no IN NUMBER) RETURN SYS_REFCURSOR;

  -- Procedure to update department details
  PROCEDURE update_department(p_department IN t_department);

  -- Procedure to delete a department
  PROCEDURE delete_department(p_dept_no IN NUMBER);
END pkg_departments;
/

-- Package body for DEPARTMENTS CRUD operations
CREATE OR REPLACE PACKAGE BODY pkg_departments IS
  PROCEDURE create_department(p_department IN OUT t_department) IS
  BEGIN
    INSERT INTO DEMO.DEPARTMENTS (NAME, LOCATION)
    VALUES (p_department.name, p_department.location)
    RETURNING DEPT_NO INTO p_department.dept_no;
  END create_department;

  FUNCTION get_department(p_dept_no IN NUMBER) RETURN SYS_REFCURSOR IS
    v_cursor SYS_REFCURSOR;
  BEGIN
    OPEN v_cursor FOR
      SELECT *
      FROM DEMO.DEPARTMENTS
      WHERE DEPT_NO = p_dept_no;
    RETURN v_cursor;
  END get_department;

  PROCEDURE update_department(p_department IN t_department) IS
  BEGIN
    UPDATE DEMO.DEPARTMENTS
    SET NAME = p_department.name,
        LOCATION = p_department.location
    WHERE DEPT_NO = p_department.dept_no;
  END update_department;

  PROCEDURE delete_department(p_dept_no IN NUMBER) IS
  BEGIN
    DELETE FROM DEMO.DEPARTMENTS WHERE DEPT_NO = p_dept_no;
  END delete_department;
END pkg_departments;
/
