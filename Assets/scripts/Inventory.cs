[System.Serializable]
public class Inventory {
    public int nbLightSourcesCurrent;
    public int nbLightSourcesMax;
    public int nbCratesCurrent;
    public int nbCratesMax;

    public Square.Type selectedItem;

    public Inventory(int nbLightSourcesMax, int nbCratesMax, Square.Type selectedItem) {
        this.nbLightSourcesCurrent = nbLightSourcesMax;
        this.nbLightSourcesMax = nbLightSourcesMax;
        this.nbCratesCurrent = nbCratesMax;
        this.nbCratesMax = nbCratesMax;
        this.selectedItem = selectedItem;
    }
}
