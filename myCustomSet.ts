export class MyCustomSet {
    set: any[]

    constructor(defaultSet: any[]){
        this.set = defaultSet;
    }

    intersectWith(customSet:MyCustomSet){
        let intersectValues : any[];

        this.set.forEach(element => {
            if(customSet.set.includes(element)){
                intersectValues.push(element);
            }
        });

        return intersectValues;
    }

    unionWith(customSet: MyCustomSet) : any[] {
        let union = this.set;

        union.push(customSet);

        return union;
    }

    isSubsetOf(customSet: MyCustomSet) : boolean {
        return customSet.set.includes(this.set);
    }

    isSupersetOf(customSet: MyCustomSet) : boolean {
        return this.set.includes(customSet.set);
    }

    getDifference(customSet: MyCustomSet) : any[]{
        let differentValues : any[];

        this.set.forEach(element => {
            if(!customSet.set.includes(element)){
                differentValues.push(element);
            }
        });

        customSet.set.forEach(element => {
            if(!this.set.includes(element)){
                differentValues.push(element);
            }
        });

        return differentValues;
    }

    symmetricDifferenceWith(customSet: MyCustomSet) : any[]{
        let diff = this.set.filter(x => !customSet.set.includes(x) );

        return diff;
    }
}

class Client{
    doThis(){

        let set : any[]
        let set2 : any[]

        set = [1, 2, 5, "test"]
        set2 = [1, 2, 3, "test"]
        let myset = new MyCustomSet(set)

        console.log(myset.getDifference(new MyCustomSet(set2)))
    }
}