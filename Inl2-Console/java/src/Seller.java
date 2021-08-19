//import java.util.*;
    
    public class Seller {
        String name; 
        String id;
        int soldItems; 
        public District district; 
        SalaryLevel salaryLevel;


        public Seller(String name, String id, District district, int soldItems) {
            this.name = name; this.id = id; district = this.district; this.soldItems = soldItems; 
            setSalaryLevel();
        }

        private void setSalaryLevel(){
            if (this.soldItems < 50) this.salaryLevel = SalaryLevel.ONE;
            else if (this.soldItems < 100) this.salaryLevel = SalaryLevel.TWO;
            else if (this.soldItems < 200) this.salaryLevel = SalaryLevel.THREE;
            else this.salaryLevel = SalaryLevel.FOUR;
        }

        @Override
        public String toString(){
            return this.name + "\t" + this.id + "\t" + this.soldItems;
        }
    }