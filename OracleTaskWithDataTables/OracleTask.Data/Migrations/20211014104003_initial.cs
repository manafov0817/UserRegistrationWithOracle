using Microsoft.EntityFrameworkCore.Migrations;

namespace OracleTask.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                  @"CREATE OR REPLACE PACKAGE ORACLETASK.PKC_LOACTION  IS   
                    PROCEDURE  ADD_LOCATION 
                    (location_latitude LOCATIONS.LATITUDE%TYPE,
                    location_longitude LOCATIONS.LONGITUDE%TYPE,
                    location_markas LOCATIONS.MARKAS%TYPE,
                    location_userid LOCATIONS.User_Id%TYPE);

                    FUNCTION EDIT(
                    location_id LOCATIONS.ID%TYPE,
                    location_latitude LOCATIONS.LATITUDE%TYPE,
                    location_longitude LOCATIONS.LONGITUDE%TYPE,
                    location_markas LOCATIONS.MARKAS%TYPE)
                    RETURN NUMBER;

                    FUNCTION DELETE_LOCATION(location_id LOCATIONS.ID%TYPE) RETURN NUMBER;

                    FUNCTION GET_ALL_LOCATIONS return SYS_REFCURSOR;

                    FUNCTION GET_BY_ID (ID NUMBER) RETURN SYS_REFCURSOR;

                    FUNCTION GET_BY_USER_ID (user_select_id NUMBER) RETURN SYS_REFCURSOR;

                    FUNCTION EXITS_BY_ID (ID NUMBER) RETURN NUMBER;

                    END PKC_LOACTION;
                  




                    CREATE OR REPLACE PACKAGE ORACLETASK.PKC_CITY  IS 
 
                    PROCEDURE  ADD_CITY 
                    (city_name CITIES.NAME%TYPE,
                    country_name CITIES.COUNTRYNAME%TYPE); 

                    FUNCTION GET_ALL_CITIES RETURN SYS_REFCURSOR;

                    FUNCTION GET_BY_ID(city_id NUMBER) RETURN SYS_REFCURSOR;

                    FUNCTION EXITS_BY_ID (city_id NUMBER) RETURN NUMBER;

                    FUNCTION EDIT(
                    city_id CITIES.ID%TYPE,
                    city_name CITIES.NAME%TYPE,
                    country_name CITIES.COUNTRYNAME%TYPE)
                    RETURN NUMBER;

                    FUNCTION DELETE_CITY(city_id CITIES.ID%TYPE) RETURN NUMBER;

                    END PKC_CITY;
                    




                    CREATE OR REPLACE PACKAGE ORACLETASK.PKC_IMAGE IS 
 
                    PROCEDURE  ADD_IMAGE
                    (image_imagename IMAGES.IMAGE_NAME%TYPE,
                    image_userid IMAGES.USER_ID%TYPE);

                    FUNCTION EDIT(
                    image_id IMAGES.ID%TYPE,
                    image_imagename IMAGES.IMAGE_NAME%TYPE)
                    RETURN NUMBER;

                    FUNCTION DELETE_IMAGE(image_id IMAGES.ID%TYPE) RETURN NUMBER;

                    FUNCTION GET_ALL_IMAGES return SYS_REFCURSOR;

                    FUNCTION GET_BY_ID (ID NUMBER) RETURN SYS_REFCURSOR;

                    FUNCTION GET_BY_USER_ID (user_select_id NUMBER) RETURN SYS_REFCURSOR;

                    FUNCTION EXITS_BY_ID (ID NUMBER) RETURN NUMBER;

                    END PKC_IMAGE;




                    CREATE OR REPLACE PACKAGE ORACLETASK.PKC_USER  IS  
 
                    FUNCTION GET_ALL_USERS RETURN SYS_REFCURSOR;

                    FUNCTION GET_ALL_USERS_WITH_PROPERTIES RETURN SYS_REFCURSOR;

                    FUNCTION GET_USER_WITH_PROPERTIES_BY_ID (user_select_id NUMBER) RETURN SYS_REFCURSOR;

                    FUNCTION GET_BY_ID (ID NUMBER) RETURN SYS_REFCURSOR;
 
                    FUNCTION GET_ID_BY_USERNAME  (USERNAME users.USER_USERNAME%TYPE) RETURN NUMBER;

                    FUNCTION EXITS_BY_ID (ID NUMBER )
	                    RETURN NUMBER;

                    FUNCTION EDIT(
                    usr_id users.USER_ID%TYPE,
                    usr_name users.USER_NAME%TYPE,
                    usr_surname users.USER_SURNAME%TYPE,
                    usr_username users.USER_USERNAME%TYPE,
                    usr_email users.USER_EMAIL%TYPE,
                    usr_password users.USER_PASSWORD%TYPE)
                    RETURN NUMBER;

                    FUNCTION DELETE_USER(
                    usr_id users.USER_ID%TYPE)
                    RETURN NUMBER;

                    PROCEDURE  ADD_USER
                    (usr_name users.USER_NAME%TYPE, usr_surname users.USER_SURNAME%TYPE, usr_username users.USER_USERNAME%TYPE, 
                    usr_email users.USER_EMAIL%TYPE, usr_password users.USER_PASSWORD%TYPE);

                    END PKC_USER;");

            migrationBuilder.Sql(@"
                            CREATE OR REPLACE PACKAGE BODY ORACLETASK.PKC_CITY
                            IS 


                            PROCEDURE  ADD_CITY
                            (city_name CITIES.NAME%TYPE,
                            country_name CITIES.COUNTRYNAME%TYPE)
                            is 
                            begin 
                            insert into
                            CITIES(NAME , COUNTRYNAME)
                            values  (city_name, country_name);
                            end ADD_CITY;

                            FUNCTION GET_ALL_CITIES RETURN SYS_REFCURSOR IS
                                U_CURSOR SYS_REFCURSOR;
                                BEGIN
                                OPEN U_CURSOR FOR
                                SELECT 
                                ID,
                                NAME,
                                COUNTRYNAME 
                                FROM ORACLETASK.CITIES 
   	                            ORDER BY ID;
   
                                RETURN U_CURSOR;
                                END;


                            FUNCTION GET_BY_ID (city_id NUMBER) 
                                RETURN SYS_REFCURSOR is
                                U_CURSOR SYS_REFCURSOR;
                                begin
                                open U_CURSOR for
                                SELECT 
                                ID,
                                NAME,
                                COUNTRYNAME 
                                FROM ORACLETASK.CITIES 
                                WHERE ID = city_id;   
                                return U_CURSOR;
                                end;
 
 
  
 
                            FUNCTION EXITS_BY_ID (city_id NUMBER )
	                            RETURN NUMBER is
                                CITY_EXIST NUMBER:=0;   
                                CITY_COUNT NUMBER;
                                BEGIN 
	 
                                select count(*) AS count1
                                into   CITY_COUNT
                                from   ORACLETASK.CITIES 
                                WHERE ID=ID;
	    
   
                                IF CITY_COUNT>0 THEN
		                            CITY_EXIST:=1;
                                ELSE 
  		                            CITY_EXIST:=0;
                                END IF; 
   
                                RETURN CITY_EXIST;
                            END; 


                            FUNCTION EDIT(
                            city_id CITIES.ID%TYPE,
                            city_name CITIES.NAME%TYPE,
                            country_name CITIES.COUNTRYNAME%TYPE)
                            RETURN NUMBER AS 
                            isChanged NUMBER:=0;
                            BEGIN
	                            UPDATE ORACLETASK.CITIES 
	                            SET 
	                            NAME = city_name,
	                            COUNTRYNAME =country_name
	                            WHERE ID = city_id;
 	                            RETURN isChanged;
                            END;

                            FUNCTION DELETE_CITY(city_id CITIES.ID%TYPE)
                            RETURN NUMBER
                            AS 
                            isDeleted NUMBER:=0;
                            BEGIN
	                            DELETE FROM ORACLETASK.CITIES 
	                            WHERE ID = city_id;

 	                            RETURN isDeleted;
                            END;


                            END PKC_CITY;");

            migrationBuilder.Sql(@"
                                CREATE OR REPLACE PACKAGE BODY ORACLETASK.PKC_IMAGE IS  

                                PROCEDURE  ADD_IMAGE
                                (image_imagename IMAGES.IMAGE_NAME%TYPE,
                                image_userid IMAGES.USER_ID%TYPE)
                                is 
                                begin 
                                insert into
                                Images(IMAGE_NAME, USER_ID)
                                values  (image_imagename, image_userid);
                                end ADD_IMAGE;


                                FUNCTION GET_ALL_IMAGES return SYS_REFCURSOR
                                IS U_CURSOR SYS_REFCURSOR;
                                  begin
                                    open U_CURSOR for
                                    SELECT 
                                    i.ID,
                                    i.IMAGE_NAME,
                                    u.USER_USERNAME UserName
                                    FROM ORACLETASK.IMAGES i
                                    INNER JOIN ORACLETASK.USERS u  
                                    On i.USER_ID = u.USER_ID 
   	                                ORDER BY i.ID ;
	                                return U_CURSOR;
                                  end;



                                FUNCTION GET_BY_ID (ID NUMBER) RETURN SYS_REFCURSOR IS
                                    U_CURSOR SYS_REFCURSOR;
                                  BEGIN
                                    OPEN U_CURSOR FOR    
                                    SELECT 
                                    i.ID,
                                    i.IMAGE_NAME
                                    FROM ORACLETASK.IMAGES i
                                    WHERE i.ID = ID;
                                    RETURN U_CURSOR;
                                END;
 
                                FUNCTION GET_BY_USER_ID (user_select_id NUMBER)
                                RETURN SYS_REFCURSOR IS 
                                U_CURSOR SYS_REFCURSOR;
                                  BEGIN
                                    OPEN U_CURSOR FOR    
                                    SELECT 
                                    i.ID,
                                    i.IMAGE_NAME
                                    FROM ORACLETASK.IMAGES i
                                    WHERE i.USER_ID = user_select_id;
                                    RETURN U_CURSOR;
                                END; 

  
 
                                FUNCTION EXITS_BY_ID (ID NUMBER) RETURN NUMBER
                                is
                                    IMAGE_EXIST NUMBER:=0;   
                                     IMAGE_COUNT NUMBER;

	                                BEGIN 	 
	                                    select count(*) AS count1
	                                    into   IMAGE_COUNT
	                                    from   ORACLETASK.IMAGES i
	                                    WHERE i.ID=ID;
	    
   
                                IF IMAGE_COUNT>0 THEN
		                                IMAGE_EXIST:=1;
                                   ELSE 
  		                                IMAGE_EXIST:=0;
                                END IF; 
   
                                    RETURN IMAGE_EXIST;
                                END; 

                                FUNCTION EDIT(
                                image_id IMAGES.ID%TYPE,
                                image_imagename IMAGES.IMAGE_NAME%TYPE)
                                RETURN NUMBER AS 
                                isChanged NUMBER:=0;
                                BEGIN
	                                UPDATE ORACLETASK.IMAGES 
	                                SET 
	                                IMAGE_NAME = image_imagename
	                                WHERE ID = image_id;
 	                                RETURN isChanged;
                                END;

                                FUNCTION DELETE_IMAGE(image_id IMAGES.ID%TYPE)
                                RETURN NUMBER
                                AS 
                                isDeleted NUMBER:=0;
                                BEGIN
	                                DELETE FROM ORACLETASK.IMAGES
	                                WHERE ID = image_id;
 	                                RETURN isDeleted;
                                END;

                                END PKC_IMAGE;");

            migrationBuilder.Sql(@"
                                CREATE OR REPLACE PACKAGE BODY ORACLETASK.PKC_LOACTION
                                IS 


                                PROCEDURE  ADD_LOCATION 
                                (location_latitude LOCATIONS.LATITUDE%TYPE,
                                location_longitude LOCATIONS.LONGITUDE%TYPE,
                                location_markas LOCATIONS.MARKAS%TYPE,
                                location_userid LOCATIONS.User_Id%TYPE)
                                is 
                                begin 
                                insert into
                                Locations(LATITUDE, LONGITUDE, MARKAS, USER_ID)
                                values  (location_latitude, location_longitude, location_markas,location_userid);
                                end ADD_LOCATION;


                                FUNCTION GET_ALL_LOCATIONS return SYS_REFCURSOR is
                                    U_CURSOR SYS_REFCURSOR;
                                  begin
                                    open U_CURSOR for
                                    SELECT 
                                    l.ID,
                                    l.LATITUDE,
                                    l.LONGITUDE,
                                    l.MARKAS, 
                                    u.USER_USERNAME UserName
                                    FROM ORACLETASK.LOCATIONS l
                                    INNER JOIN ORACLETASK.USERS u  
                                    On l.USER_ID = u.USER_ID 
   	                                ORDER BY l.ID ;
	                                return U_CURSOR;
                                  end;


                                FUNCTION GET_BY_ID (ID NUMBER) 
                                  RETURN SYS_REFCURSOR IS
                                    U_CURSOR SYS_REFCURSOR;
                                  BEGIN
                                    OPEN U_CURSOR FOR    
                                    SELECT 
                                    l.ID,
                                    l.LATITUDE,
                                    l.LONGITUDE,
                                    l.MARKAS,
                                    u.USER_USERNAME UserName
                                    FROM ORACLETASK.LOCATIONS l
                                    INNER JOIN ORACLETASK.USERS u  
                                    On l.USER_ID = u.USER_ID
                                    WHERE l.ID = ID
   	                                ORDER BY l.ID;   
                                    RETURN U_CURSOR;
                                END;

                                FUNCTION GET_BY_USER_ID (user_select_id NUMBER)
                                RETURN SYS_REFCURSOR IS
                                    U_CURSOR SYS_REFCURSOR;
                                  BEGIN
                                    OPEN U_CURSOR FOR    
                                    SELECT 
                                    l.ID,
                                    l.LATITUDE,
                                    l.LONGITUDE,
                                    l.MARKAS
                                    FROM ORACLETASK.LOCATIONS l
                                    WHERE l.USER_ID = user_select_id;

                                    RETURN U_CURSOR;
                                END;
 
  
 
                                FUNCTION EXITS_BY_ID (ID NUMBER )
	                                RETURN NUMBER is
                                    LOCATION_EXIST NUMBER:=0;   
                                    LOCATION_COUNT NUMBER;

	                                BEGIN 	 
	                                    select count(*) AS count1
	                                    into   LOCATION_COUNT
	                                    from   ORACLETASK.LOCATIONS l 
	                                    WHERE l.ID=ID;
	    
   
                                IF LOCATION_COUNT>0 THEN
		                                LOCATION_EXIST:=1;
                                   ELSE 
  		                                LOCATION_EXIST:=0;
                                END IF; 
   
                                    RETURN LOCATION_EXIST;
                                END; 

                                FUNCTION EDIT(
                                location_id LOCATIONS.ID%TYPE,
                                location_latitude LOCATIONS.LATITUDE%TYPE,
                                location_longitude LOCATIONS.LONGITUDE%TYPE,
                                location_markas LOCATIONS.MARKAS%TYPE)
                                RETURN NUMBER AS 
                                isChanged NUMBER:=0;
                                BEGIN
	                                UPDATE ORACLETASK.LOCATIONS 
	                                SET 
	                                LATITUDE = location_latitude,
	                                LONGITUDE = location_longitude,
	                                MARKAS = location_markas
	                                WHERE ID = location_id;
 	                                RETURN isChanged;
                                END;

                                FUNCTION DELETE_LOCATION(location_id LOCATIONS.ID%TYPE)
                                RETURN NUMBER
                                AS 
                                isDeleted NUMBER:=0;
                                BEGIN
	                                DELETE FROM ORACLETASK.LOCATIONS 
	                                WHERE ID = location_id;
 	                                RETURN isDeleted;
                                END;

                                END PKC_LOACTION;");

            migrationBuilder.Sql(@"
                                CREATE OR REPLACE PACKAGE BODY ORACLETASK.PKC_USER
                                IS 

                                function GET_ALL_USERS return SYS_REFCURSOR is
                                    U_CURSOR SYS_REFCURSOR;
                                    begin
                                    open U_CURSOR for
                                    SELECT 
                                    USER_ID,
                                    USER_NAME, 
                                    USER_SURNAME,
                                    USER_USERNAME,
                                    USER_EMAIL,
                                    USER_PASSWORD
                                    FROM USERS
   	                                ORDER BY USER_ID ;
   
                                    return U_CURSOR;
                                    end;

                                    function GET_ALL_USERS_WITH_PROPERTIES return SYS_REFCURSOR is
                                    U_CURSOR SYS_REFCURSOR;
                                BEGIN
                                    open U_CURSOR for
                                    SELECT 
                                    u.USER_ID,
                                    u.USER_NAME,
                                    u.USER_SURNAME,
                                    u.USER_USERNAME,
                                    u.USER_EMAIL,
                                    u.USER_PASSWORD,
                                    l.LATITUDE,
                                    l.LONGITUDE,
                                    l.MARKAS,
                                    i.IMAGE_NAME 
                                    FROM USERS u
                                    INNER JOIN ORACLETASK.IMAGES i 
                                    ON i.USER_ID = u.USER_ID
                                    INNER JOIN ORACLETASK.LOCATIONS l 
                                    ON l.USER_ID = u.USER_ID 
   	                                ORDER BY USER_ID ;
   
                                    return U_CURSOR;
                                    end;

                                FUNCTION GET_USER_WITH_PROPERTIES_BY_ID (user_select_id NUMBER)
                                return SYS_REFCURSOR is
                                    U_CURSOR SYS_REFCURSOR;
                                BEGIN
                                    OPEN U_CURSOR FOR
                                    SELECT 
                                    u.USER_ID,
                                    u.USER_NAME,
                                    u.USER_SURNAME,
                                    u.USER_USERNAME,
                                    u.USER_EMAIL,
                                    u.USER_PASSWORD,
                                    l.LATITUDE,
                                    l.LONGITUDE,
                                    l.MARKAS,
                                    i.IMAGE_NAME 
                                    FROM USERS u
                                    INNER JOIN ORACLETASK.IMAGES i 
                                    ON i.USER_ID = u.USER_ID
                                    INNER JOIN ORACLETASK.LOCATIONS l 
                                    ON l.USER_ID = u.USER_ID 
                                    WHERE u.USER_ID = user_select_id;
   
                                    return U_CURSOR;
                                    end;

                                FUNCTION GET_BY_ID (ID NUMBER) 
                                    RETURN SYS_REFCURSOR is
                                    U_CURSOR SYS_REFCURSOR;
                                    begin
                                    open U_CURSOR for
                                    SELECT 
                                    USER_ID,
                                    USER_NAME,
                                    USER_SURNAME,
                                    USER_USERNAME,
                                    USER_EMAIL,
                                    USER_PASSWORD
                                    FROM USERS
                                    WHERE USER_ID = ID;
   
                                    return U_CURSOR;
                                    end;
 
                                    FUNCTION GET_ID_BY_USERNAME (USERNAME users.USER_USERNAME%TYPE)
                                    RETURN NUMBER is
                                        RETURN_ID NUMBER:=0;
                                    begin
   	 
                                    select u.USER_ID 
                                    into   RETURN_ID
                                    from   USERS u
                                    WHERE u.USER_USERNAME =USERNAME;
   
                                    return RETURN_ID;
                                    end;  
 
                                FUNCTION EXITS_BY_ID (ID NUMBER )
	                                RETURN NUMBER is
                                    USER_EXIST NUMBER:=0;   
                                    USER_COUNT NUMBER;

                                    BEGIN 
	 
                                    select count(*) AS count1
                                    into   USER_COUNT
                                    from   USERS
                                    WHERE USER_ID=ID;
                                    IF USER_COUNT>0 THEN
		                                USER_EXIST:=1;
                                    ELSE 
  		                                USER_EXIST:=0;
                                    END IF;   
                                    RETURN USER_EXIST;
                                END; 


                                FUNCTION EDIT(
                                usr_id users.USER_ID%TYPE,
                                usr_name users.USER_NAME%TYPE,
                                usr_surname users.USER_SURNAME%TYPE,
                                usr_username users.USER_USERNAME%TYPE,
                                usr_email users.USER_EMAIL%TYPE, 
                                usr_password users.USER_PASSWORD%TYPE)
                                RETURN NUMBER AS 
                                isChanged NUMBER:=0;
                                BEGIN
	                                UPDATE USERS 
	                                SET 
	                                USER_NAME =usr_name,
	                                USER_SURNAME =usr_surname,
	                                USER_USERNAME = usr_username,
	                                USER_EMAIL = usr_email,
	                                USER_PASSWORD = usr_password
	                                WHERE USER_ID = usr_id;


 	                                RETURN isChanged;
                                END;

                                FUNCTION DELETE_USER(usr_id users.USER_ID%TYPE)
                                RETURN NUMBER
                                AS 
                                isDeleted NUMBER:=0;
                                BEGIN
	                                DELETE FROM USERS 
	                                WHERE USER_ID = usr_id;

 	                                RETURN isDeleted;
                                END;



                                PROCEDURE ADD_USER
                                (usr_name users.USER_NAME%TYPE, usr_surname users.USER_SURNAME%TYPE, usr_username users.USER_USERNAME%TYPE, 
                                usr_email users.USER_EMAIL%TYPE, usr_password users.USER_PASSWORD%TYPE)
                                is 
                                begin 
                                insert into
                                users(USER_NAME , USER_SURNAME,    USER_USERNAME,   USER_EMAIL,   USER_PASSWORD )
                                values  (usr_name, usr_surname, usr_username,  usr_email, usr_password);
                                end add_user;
                                END PKC_USER;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
