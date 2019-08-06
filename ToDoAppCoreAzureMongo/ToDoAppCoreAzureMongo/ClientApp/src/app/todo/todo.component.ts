import { Component, OnInit } from '@angular/core';
import { TodoService } from '../todo.service';
import { todo } from '../classes/todo';



@Component({
  selector: 'app-todo',
  templateUrl: './todo.component.html',
  styleUrls: ['./todo.component.css']
})
export class TodoComponent implements OnInit {

  lijst
  constructor(private todoService: TodoService) {
   // this.lijst = todoService.todoLijst;
   // this.lijst = [];
    todoService.get().subscribe(arg => {
      console.log(arg);
      this.lijst = arg;
      console.log(this.lijst);
//      this.lijst.push();
    });
  }

  ngOnInit() {
  }
  add(arg) {
    console.log(arg.value.item);
    this.todoService.post(new todo(arg.value.item, false));
    //this.lijst.push(new todo(arg.value.item, false));
  }

}
