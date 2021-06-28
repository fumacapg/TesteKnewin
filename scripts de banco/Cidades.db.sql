BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
	"MigrationId"	TEXT NOT NULL,
	"ProductVersion"	TEXT NOT NULL,
	CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY("MigrationId")
);
CREATE TABLE IF NOT EXISTS "Cidades" (
	"Id"	INTEGER NOT NULL,
	"Nome"	TEXT,
	"NumeroHabitantes"	INTEGER NOT NULL,
	CONSTRAINT "PK_Cidades" PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Fronteiras" (
	"Id"	INTEGER NOT NULL,
	"CidadeFronteira"	TEXT,
	"CidadeId"	INTEGER NOT NULL,
	CONSTRAINT "PK_Fronteiras" PRIMARY KEY("Id" AUTOINCREMENT),
	CONSTRAINT "FK_Fronteiras_Cidades_CidadeId" FOREIGN KEY("CidadeId") REFERENCES "Cidades"("Id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "Usuarios" (
	"Id"	INTEGER NOT NULL,
	"Login"	TEXT,
	"Senha"	TEXT,
	"Role"	TEXT,
	CONSTRAINT "PK_Usuarios" PRIMARY KEY("Id" AUTOINCREMENT)
);
INSERT INTO "__EFMigrationsHistory" ("MigrationId","ProductVersion") VALUES ('20210627210252_Inicial','5.0.6'),
 ('20210628012519_Usuarios','5.0.6');
INSERT INTO "Cidades" ("Id","Nome","NumeroHabitantes") VALUES (1,'Praia Grande',350000),
 (2,'São Vicente',223597),
 (3,'Itanhaém',180000),
 (4,'Santos',400000),
 (5,'Mongagua',180000);
INSERT INTO "Fronteiras" ("Id","CidadeFronteira","CidadeId") VALUES (1,'Mongagua',1),
 (2,'São Vicente',1),
 (3,'Praia Grande',2),
 (4,'Cubatão',2),
 (5,'Santos',2),
 (6,'São Vicente',4),
 (7,'Guarujá',4),
 (8,'Cubatão',4),
 (9,'Praia Grande',5),
 (10,'Itanhaem',5),
 (11,'São Paulo',5);
INSERT INTO "Usuarios" ("Id","Login","Senha","Role") VALUES (1,'carlos','$6$Eg/sL4EZw0juXiib$pCrfPbEejy5nTYgn.W3NdwCYx1942MNbFxYYshhBMB19tCViU2W3eQz/RiMffW8fJk5hZs.2nCso203GMjd5e1','Administrador'),
 (3,'knewin','$6$J.vZYf5Mi2gCgpwH$bEoahW61fiOIiSFAbwMieTjShR.zYbqa4KhqrlX4cX0046gtw4f/cVitPwpxsItMjN78Vubqpye2JECuyHXa2/','Administrador');
CREATE INDEX IF NOT EXISTS "IX_Fronteiras_CidadeId" ON "Fronteiras" (
	"CidadeId"
);
COMMIT;
