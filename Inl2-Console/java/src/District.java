    import java.util.*;
    import com.google.common.collect.ArrayListMultimap;
    import com.google.common.collect.ListMultimap;

    public class District {

        // instance fields...
        String name;
        LinkedList<Seller> sellers;

        public District(String name){
            //var sellerList = new ArrayList<Seller>();
            SalaryLevel[] sl = SalaryLevel.values();
            this.sellers = new LinkedList<Seller>();
            this.name = name;
        }
        
        public void addSeller(Seller s){
            //if(this.sellers.get(s.salaryLevel).isEmpty()) System.out.println("knull");
            this.sellers.add(s);
        }

        // https://github.com/google/guava/wiki/NewCollectionTypesExplained#multimap
        // https://stackoverflow.com/questions/57243635/vscode-can-not-import-external-libraries
        public HashMap<SalaryLevel, LinkedList<Seller>> sortSellerBySalaryLevel(){

            SalaryLevel[] sl = SalaryLevel.values();
            //Seller[] sellers = this.sellers.toArray(new Seller[this.sellers.size()]);
            var map = new HashMap<SalaryLevel, LinkedList<Seller>>();
            var list = new LinkedList<Seller>(this.sellers);
            for(Seller s : this.sellers){
                //map.add brutalt jobbigt utan multimap
            }

            // kan lätt göra en ström av det ju, med lambda uttryck
            return null;
        }

        @Override
        public String toString(){
            String s = "";
            s += "District: \t" + this.name + "\n" + "\tSellers: \n";
            for(Map.Entry<SalaryLevel, LinkedList<Seller>> entry : sellers.entrySet()){
                for(Seller seller : entry.getValue()){
                s += seller.toString() + "\n";
                }
            }
            
            return s;
        }
    }