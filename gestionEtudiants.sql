

create table Filiere(
Id_filiere int primary key identity(1,1),
Nom_filiere varchar(100))

create table Etudiant(
cne int primary key,
nom varchar(100),
prenom varchar(100),
sexe char,
date_naiss date,
photo varchar(100),
email varchar(100),
id_fil int
constraint fk_fili foreign key(id_fil) references Filiere(Id_filiere)
ON DELETE CASCADE,
CONSTRAINT UQ_cne UNIQUE (cne))