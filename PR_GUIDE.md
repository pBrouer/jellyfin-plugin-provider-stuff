# Jellyfin plugin catalog (your fork)

Repo: **pBrouer/jellyfin-plugin-provider-stuff** — branch **main**

## Push and publish (1.2.1.1)

```powershell
cd "c:\Users\PBrouer\Desktop\Homelab\jellyfin-plugin-provider-stuff-upstream"
git push origin main
```

If `gh` is installed and authenticated:

```powershell
gh release create 1.2.1.1 "dist\providerstuff-1.2.1.1.zip" --title "1.2.1.1 — Config page button fix" --notes-file RELEASE_NOTES_1.2.1.1.md
```

Otherwise create the release manually:

1. https://github.com/pBrouer/jellyfin-plugin-provider-stuff/releases/new
2. Tag: **1.2.1.1**
3. Title: **1.2.1.1 — Config page button fix**
4. Upload: `dist\providerstuff-1.2.1.1.zip`
5. Paste notes from `RELEASE_NOTES_1.2.1.1.md`
6. Publish

## Jellyfin

`https://raw.githubusercontent.com/pBrouer/jellyfin-plugin-provider-stuff/main/manifest.json`

Catalog → ProviderStuff → Update to **1.2.1.1**
