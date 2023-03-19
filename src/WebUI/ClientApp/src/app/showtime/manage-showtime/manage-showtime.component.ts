import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { catchError, EMPTY, tap } from 'rxjs';
import { CreateShowTimeCommand, MovieClient, ShowTimeClient, ShowTimeDto, TheatreClient, UpdateShowTimeCommand } from 'src/app/web-api-client';

@Component({
  selector: 'app-manage-showtime',
  templateUrl: './manage-showtime.component.html',
  styleUrls: ['./manage-showtime.component.scss']
})
export class ManageShowtimeComponent implements OnInit {

  op = 'Add';

  myForm: FormGroup;

  showtimeDto: ShowTimeDto;
  
  constructor(private fb: FormBuilder,
    private showtimeClient: ShowTimeClient,
    private movieClient: MovieClient,
    private theatreClient: TheatreClient) { }

  movies$ = this.movieClient.get()
   .pipe(
      tap(movies => console.log(movies)),
      catchError(err => {
        console.log(err);
        return EMPTY;
      })
   )

   theatres$ = this.theatreClient.get()
    .pipe(
        tap(t => console.log(t)),
        catchError(err => {
          console.log(err);
          return EMPTY;
        })
   )   

  ngOnInit() {

    this.showtimeDto = new ShowTimeDto();

    this.myForm = this.fb.group({
      datetime: ['', Validators.required],
      price: ['', Validators.required],
      movieId: ['', Validators.required],
      theatreId: ['', Validators.required]
    });
    
  }
  
  onSubmit() {
    
    if (this.op === 'Add') {
      // send request to server
      let cmdCreate = new CreateShowTimeCommand();
      cmdCreate.dateTime = new Date(this.showtimeDto.dateTime);
      cmdCreate.price = this.showtimeDto.price;
      cmdCreate.movieId = this.showtimeDto.movieId;
      cmdCreate.theatreId = this.showtimeDto.theatreId;
    
      this.showtimeClient.create(cmdCreate)
        .subscribe(response => {
          if (response) {
            alert('Show time created successfully');
            this.myForm.reset();
          }
        },
          error => {
            alert(JSON.parse(error.response).errors['Title']);
          });
    } else {

      let cmdUpdate = new UpdateShowTimeCommand();
      cmdUpdate.id = this.showtimeDto.id;
      cmdUpdate.dateTime = new Date(this.showtimeDto.dateTime);
      cmdUpdate.price = this.showtimeDto.price;
      cmdUpdate.movieId = this.showtimeDto.movieId;
      cmdUpdate.theatreId = this.showtimeDto.theatreId;

      this.showtimeClient.update(this.showtimeDto.id, cmdUpdate)
            .subscribe(response => {
              alert('Show time updated successfully');
            }, 
            error => {
              alert(JSON.parse(error.response).errors['Title']);
            });
    }

  }
}
