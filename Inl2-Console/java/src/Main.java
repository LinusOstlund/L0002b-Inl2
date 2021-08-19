import java.util.*;

public class Main {

    /* Instance fields */
    HashMap< District , HashMap<SalaryLevel, ArrayList<Seller> > > catalogue;
    String[] testNames = {"Jan Jansson", "Börje Bourgieson", "Pelle Jönssons", "Fågel Kille", "Maja Jönsson"};
    String[] id = {"9010124114", "9012101441", "9201010110", "0303034444", "0101010110"};
    int[] soldItems = {35, 50, 113, 201, 150};
    District malmo, sthlm, gbg;
    District[] districts = new District[3];
    ArrayList<Seller> sellers = new ArrayList<Seller>();

    public Main(){
        // use testdata
        malmo = new District("Malmö");
        sthlm = new District("Stockholm");
        gbg = new District("Göteborg");
        this.districts[0] = malmo;
        this.districts[1] = sthlm;
        this.districts[2] = gbg;
        int i = 0; 
        Seller s;
        for(String p : this.testNames){
            s = new Seller(p, this.id[i], this.districts[i % 3], this.soldItems[i]);
            this.sellers.add(s);
            System.out.println("hej hej!");
            //System.out.println(s.district.toString());
            System.out.println("helvete");
            s.district.addSeller(s);
            i++;
        }
    }

    public static void main(String args[]){
        Main test = new Main();
        System.out.println(test.toString());

    }

    @Override
    public String toString(){
        String s = "";
        for(District d : this.districts) {
        s += d.toString() + "\n";
        }
        return s;
    }

}