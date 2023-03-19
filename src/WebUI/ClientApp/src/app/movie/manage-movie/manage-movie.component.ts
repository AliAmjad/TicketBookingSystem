import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { catchError, EMPTY, map, Observable, Observer, of, switchMap, tap } from 'rxjs';
import { CreateMovieCommand, MovieClient, MovieDto, UpdateMovieCommand } from 'src/app/web-api-client';


interface MovieSearchResponse {
  totalResults: number;
  Search: Movie[];
}
 
interface Movie {
  Title: string;
  Year: string;
}

@Component({
  selector: 'app-manage-movie',
  templateUrl: './manage-movie.component.html',
  styleUrls: ['./manage-movie.component.css']
})
export class ManageMovieComponent implements OnInit {

  op = 'Add';

  myForm: FormGroup;
  maxImageSize: number = 5 * 1024 * 1024; // 5 MB
  imagePreview: string;

  suggestions$?: Observable<Movie[]>;

  movieDto: MovieDto;

  constructor(private formBuilder: FormBuilder,
    private movieClient: MovieClient,
    private route: ActivatedRoute,
    private http: HttpClient) {

    if (route.snapshot.params['id']) {
      movieClient.getById(route.snapshot.params['id'])
        .subscribe(response => {
          this.movieDto = response;
          this.imagePreview = this.movieDto.image;

          // change op
          this.op = 'Update';

          // clear errors as we have the image
          this.myForm.controls['image'].setErrors(null);
        });
    }
  }

  ngOnInit(): void {

    this.movieDto = new MovieDto();

    this.myForm = this.formBuilder.group({
      image: ['', [Validators.required]],
      title: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(100)]],
      description: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(500)]],
      releaseDate: ['', [Validators.required]]
    });

    this.suggestions$ = new Observable((observer: Observer<string | undefined>) => {
      observer.next(this.movieDto.title);
    }).pipe(
      switchMap((query: string) => {
        if (query) {
          // using github public api to get users by name
          return this.http.get<MovieSearchResponse>(
            'https://www.omdbapi.com/?i=tt3896198&apikey=491e7d29', {
            params: { s: query }
          }).pipe(
            map((data: MovieSearchResponse) => data && data.Search || [])
          );
        }
 
        return of([]);
      })
    );
  }

  onFileChange(event: any) {
    const file = (event.target as HTMLInputElement).files[0];
    if (file) {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => {
        this.imagePreview = reader.result as string;
        this.myForm.patchValue({ image: file });
      }
    }
  }

  onSubmit() {

    // attach image
    this.movieDto.image = this.imagePreview;

    // format date
    this.movieDto.releaseDate = new Date(this.movieDto.releaseDate);

    if (this.op === 'Add') {
      // send request to server
      let cmdCreate = new CreateMovieCommand();
      cmdCreate.title = this.movieDto.title;
      cmdCreate.description = this.movieDto.description;
      cmdCreate.releaseDate = this.movieDto.releaseDate;
      cmdCreate.image = this.movieDto.image;
  
      this.movieClient.create(cmdCreate)
        .subscribe(response => {
          if (response) {
            alert('Movie created successfully');
            this.myForm.reset();
            this.imagePreview = null;
            this.movieDto = new MovieDto();
          }
        },
          error => {
            alert(JSON.parse(error.response).errors['Title']);
          });
    } else {

      let cmdUpdate = new UpdateMovieCommand();
      cmdUpdate.id = this.movieDto.id;
      cmdUpdate.title = this.movieDto.title;
      cmdUpdate.description = this.movieDto.description;
      cmdUpdate.releaseDate = this.movieDto.releaseDate;
      cmdUpdate.image = this.movieDto.image;

      this.movieClient.update(this.movieDto.id, cmdUpdate)
            .subscribe(response => {
              alert('Movie updated successfully');
            }, 
            error => {
              alert(JSON.parse(error.response).errors['Title']);
            });
    }
  }

}
