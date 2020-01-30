CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

CREATE TABLE "Courses" (
    "Id" uuid NOT NULL,
    "DayOfWeek" text NOT NULL,
    "Name" text NOT NULL,
    "StartTime_Hour" smallint NULL,
    "StartTime_Minute" smallint NULL,
    "EndTime_Hour" smallint NULL,
    "EndTime_Minute" smallint NULL,
    "Price" numeric NOT NULL,
    CONSTRAINT "PK_Courses" PRIMARY KEY ("Id")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20200129190924_InitialMigration', '3.1.1');

