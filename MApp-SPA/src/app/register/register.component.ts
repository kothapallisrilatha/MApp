import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
model: any = {};
@Output() cancelRegister = new EventEmitter() ;

  constructor(private authservice: AuthService) { }

  ngOnInit() {
  }
register(){
  this.authservice.register(this.model).subscribe(() =>{
    console.log('Registration successful');

  }, error => {
    console.log('Failed to register!! ' + error);
  });
}
cancel(){
  this.cancelRegister.emit(false);
}
}
