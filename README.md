<p align="center">  
  <a href="https://github.com/">  
    <img alt="Geoman Logo" src="https://i.imgur.com/PpbRmg5.png" />  
  </a>  
</p>  
<h1 align="center">  
  Zero Launcher  
</h1>
<p align="center">  
  <strong>Komunitní launcher a správce modulů & jejich synchronizace pro herní titul Arma 3</strong>
  <br>Zero Launcher poskytuje řešení v oblasti komunitního hraní s moduly. Launcher zajišťuje, že všichni uživatelé stejného repozitáře obdrží vždy stejné soubory uživatelských modulů bez rozdílu.
</p>
<p align="center">
  <img height="350px" alt="Zero Launcher GUI Presentation" src="https://i.imgur.com/BnY2wEm.png">
</p>

## Dokumentace

- [Instalace](#instalace)
- [Tvorba repozitáře](#tvorba-repozitáře)
- [ZeroCore](#zerocore)

### Instalace
- Stáhněte si nejnovější vydání `Zero Launcher 1.0` na své zařízení. 
- Rozbalte ZIP archiv a celou složku umistěte do adresáře, tak aby od kořene disku až ke složce s launcherem nebyly žádné nerozpoznatelné znaky, mezery atp. Doporučeně využívejte umístění jako je např. kořen disku nebo Plocha. 
- Vytvořte si

### Tvorba repozitáře
- Stáhněte si vydání `pbo2xml-php` z [veřejného repozitáře](https://github.com/benedikz/pbo2xml-php) a umistěte obsah archivu do libovolného adresáře vašeho serveru, který je dostupný přes zabezpečený protokol HTTPS. 
- Upravte dle svých potřeb skript `defines.php`. 
  - Nastavte **identifikátor repozitáře** v **kanonickém tvaru (bez mezer, ideálně znaky [a-zA-Z, 0-9, _-])**
  - Stručný název repozitáře
  - V případě, že chcete aby při spuštění s vaším balíčkem z repozitáře měl uživatel možnost automatického připojení k hernímu serveru, vyplňte zbývající tři pole **AUTOCONNECT**
- Po úspěšném nastavení `defines.php` zkuste přistoupit k indexovacímu skriptu. Navštivte https://vase-domena/cesta/k/repozitari/indexer.php. Vaše moduly na serveru jsou nyní indexovány. Při jakékoliv manuální změně obsahu adresáře aktualizujte index tím, že k němu přistoupíte přes veřejné URL, případně si nastavte CRON job pro automatické provádění skriptu v časových intervalech.

### ZeroCore
Veškeré programy jsou obsaženy v projektové složce ZeroCore. V tomto adresáři jsou obsaženy veškeré back-end funkce launcheru spolu s objektovými modely datových celků, s nimiž launcher pracuje.

### Licence
Tento projekt je licencovaný pod [GNU General Public License v3.0](https://github.com/benedikz/zero-launcher/blob/main/LICENSE)
