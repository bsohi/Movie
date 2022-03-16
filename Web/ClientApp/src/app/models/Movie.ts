export class Movie {
  constructor() {
    this.name = "";
    this.cost = 0;
    this.salePrice = 0;
    this.genreId = 0;
    this.listValues = {
      genre: []
    };   
    
  }  
  name: string;
  id: number;
  cost: number;
  salePrice: number;
  genre: string;
  genreId: number;    
  listValues: any;  
  propertiesToUpdate: string[];

  uuid: string;
  quantity: number;
  opened: boolean;
  finalPrice: number;
}

export class item {
  id: number;
  name: string;
}
