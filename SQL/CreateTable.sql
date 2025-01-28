CREATE  TABLE DEMO.DEPARTMENTS ( 
	DEPT_NO              NUMBER GENERATED By Default AS IDENTITY (START WITH 1  INCREMENT BY 1  )  NOT NULL,
	NAME                 VARCHAR2(50)   NOT NULL,
	LOCATION             VARCHAR2(50)   ,
	CONSTRAINT PK_DEPARTMENTS PRIMARY KEY ( DEPT_NO ) 
 );
 
CREATE TABLE EMPLOYEES (  
  EMP_NO             NUMBER,  
  NAME              VARCHAR2(50) NOT NULL,  
  JOB               VARCHAR2(50),  
  MANAGER           NUMBER,  
  HIRE_DATE          DATE,  
  SALARY            NUMBER(7,2),  
  COMMISSION        NUMBER(7,2),  
  DEPT_NO           NUMBER,  
  CONSTRAINT PK_EMPLOYEES PRIMARY KEY (EMP_NO)
);