


# Problèmes rencontré :

## Connection à une bdd docker locale
Lorsque j'essaie de me connecter à une base de donnée local via docker, il faut bien préciser le serveur comme étant localhost

## L'application dotnet ne run plus via rider
Lorsque j'ai fait le ménage des version de dotnet, je me suis rendu compte qu'il y avait deux localisation :
- /usr/local/share/dotnet/dotnet
- /usr/local/share/dotnet/x64
La nouvelle norme est d'utiliser /usr/local/share/dotnet/dotnet.
Pour que ça soit effectif par rider, il faut modifier les fichiers :
- /etc/dotnet/install_location
- /etc/dotnet/install_location_x64
Et leur mettre le bon chemin.

## L'application ne compile plus le fichier généré {MonApp}.Styles.css
Lorsque je rajoute un appsettings.Local.json, le css de l'application n'est plus trouvé.
Solution pour pouvoir utiliser un environnement Local :
Rajouter dans Program.cs
``` csharp
if (builder.Environment.IsEnvironment("Local"))
{
    StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);
}
```
test

# Ressources

## Installer Bootstrap pour WebAssembly
https://docs.blazorbootstrap.com/getting-started/blazor-webapp-server-global-net-8
## Navbar verticale
### Solution 1 non retenue :
- https://dev.to/codeply/bootstrap-5-sidebar-examples-38pb
### Solution 2
- https://getbootstrap.com/docs/5.3/components/offcanvas/#live-demo
- https://getbootstrap.com/docs/5.1/components/navbar/

### Genération et gestion d'image en .net
- https://docs.sixlabors.com/articles/imagesharp.drawing/gettingstarted.html
- Image to stream : https://plavos.com/blog/imagesharp-cheatsheet/