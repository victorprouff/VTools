


Problèmes rencontré :

Lorsque j'essaie de me connecter à une base de donnée local via docker, il faut bien préciser le serveur comme étant localhost
Lorsque j'ai fait le ménage des version de dotnet, je me suis rendu compte qu'il y avait deux localisation :
- /usr/local/share/dotnet/dotnet
- /usr/local/share/dotnet/x64
La nouvelle norme est d'utiliser /usr/local/share/dotnet/dotnet.
Pour que ça soit effectif par rider, il faut modifier les fichiers :
- /etc/dotnet/install_location
- /etc/dotnet/install_location_x64
Et leur mettre le bon chemin.

Lorsque je rajoute un appsettings.Local.json, le css de l'application n'est plus trouvé.
Solution pour pouvoir utiliser un environnement Local :
Rajouter dans Program.cs
if (builder.Environment.IsEnvironment("Local"))
{
StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);
}