import { Injectable, Inject } from '@angular/core';
import { todo } from './classes/todo';
import { HttpClient } from '@angular/common/http';



@Injectable({
  providedIn: 'root'
})
export class TodoService {

  public todoLijst: todo[];
  public doneLijst: todo[];
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.todoLijst = [];
    
    this.todoLijst.push(new todo("winkelen", false));
    this.todoLijst.push(new todo("tuin klaarmaken", false));
    this.todoLijst.push(new todo("afwassen", false));

    this.doneLijst = [];
  }

  //send to server
  post(item: todo) {
   
    this.http.post(this.baseUrl + 'api/ToDo/PostToDoItem',item).subscribe(result => {
      console.log(result);
    }, error => console.error(error));
   
  }

  get() {
    return this.http.get<todo>(this.baseUrl + 'api/ToDo/GetAll');
  }

  add(item: todo) {
    console.log("add");
    console.log(item);
    this.doneLijst.push(item);
    console.log(this.doneLijst)
  }

  remove(item: todo) {

    let idx = this.doneLijst.findIndex(it => it.name == item.name);
    if (idx > 0)
      this.doneLijst.splice(idx, 1);
  }

}
