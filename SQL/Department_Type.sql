-- Creating a type for DEPARTMENTS table
CREATE OR REPLACE TYPE t_department AS OBJECT (
  dept_no    NUMBER,
  name       VARCHAR2(50),
  location   VARCHAR2(50)
);
/