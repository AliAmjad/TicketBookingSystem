import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { catchError, EMPTY } from 'rxjs';
import { CreateMovieCommand, CreateTheatreCommand, MovieClient, MovieDto, TheatreClient, TheatreDto, UpdateMovieCommand, UpdateTheatreCommand } from 'src/app/web-api-client';

@Component({
  selector: 'app-manage-theatre',
  templateUrl: './manage-theatre.component.html',
  styleUrls: ['./manage-theatre.component.css']
})
export class ManageTheatreComponent implements OnInit {

  op = 'Add';

  myForm: FormGroup;
  maxImageSize: number = 5 * 1024 * 1024; // 5 MB
  imagePreview: string;

  theatreDto: TheatreDto;

  constructor(private formBuilder: FormBuilder,
    private theaterClient: TheatreClient,
    private route: ActivatedRoute) {

    if (route.snapshot.params['id']) {
      theaterClient.getById(route.snapshot.params['id'])
        .subscribe(response => {
          this.theatreDto = response;

          // change op
          this.op = 'Update';
        });
    }
  }

  ngOnInit(): void {

    this.theatreDto = new TheatreDto();

    this.myForm = this.formBuilder.group({
      name: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(100)]],
      location: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(500)]],
      seatingCapacity: ['', [Validators.required]]
    });
  }

  onSubmit() {

    if (this.op === 'Add') {
      // send request to server
      let cmdCreate = new CreateTheatreCommand();
      cmdCreate.name = this.theatreDto.name;
      cmdCreate.location = this.theatreDto.location;
      cmdCreate.seatingCapacity = this.theatreDto.seatingCapacity;

      this.theaterClient.create(cmdCreate)
        .subscribe(response => {
          if (response) {
            alert('Theatre created successfully');
            this.myForm.reset();
            this.theatreDto = new TheatreDto();
          }
        },
          error => {
            alert(JSON.parse(error.response).errors['Title']);
          });
    } else {

      let cmdUpdate = new UpdateTheatreCommand();
      cmdUpdate.id = this.theatreDto.id;
      cmdUpdate.name = this.theatreDto.name;
      cmdUpdate.location = this.theatreDto.location;
      cmdUpdate.seatingCapacity = this.theatreDto.seatingCapacity;

      this.theaterClient.update(this.theatreDto.id, cmdUpdate)
            .subscribe(response => {
              alert('Theatre updated successfully');
            }, 
            error => {
              alert(JSON.parse(error.response).errors['Title']);
            });
    }
  }

}
