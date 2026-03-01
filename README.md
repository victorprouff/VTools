# VTools

Application web personnelle (Blazor Server / .NET 10) déployée via Dokploy.

## Backups

Les données (base SQLite) sont stockées dans un volume Docker nommé `vtools-j7zw7k_vtools-data`, géré via la fonctionnalité **Volume Backup** de Dokploy.

### Créer un backup

Dans Dokploy : service **vtools** → onglet **Backups** → **Run Backup**.

### Restaurer un backup

> La restauration remplace intégralement les données existantes.

**1. Stopper le service**
Dans Dokploy : service **vtools** → **Stop**.

**2. Supprimer le container et le volume existants** depuis le terminal hostinger (ou dokploy si ça marche)
```bash
docker rm 52bb7a35d195
docker volume rm vtools-j7zw7k_vtools-data
```
> L'ID du container peut changer après un redéploiement. Pour le retrouver :
> ```bash
> docker ps -a | grep vtools
> ```

**3. Lancer la restauration**
Dans Dokploy : onglet **Backups** → sélectionner le backup → **Restore**.

**4. Redémarrer le service**
Dans Dokploy : service **vtools** → **Start**.
