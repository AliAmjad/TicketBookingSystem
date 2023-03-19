import { Component, OnInit } from '@angular/core';
import { catchError, EMPTY, forkJoin, Observable, tap } from 'rxjs';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { AuthClient, MovieClient, UserInfoDto } from '../web-api-client';

@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.scss']
})
export class MovieComponent implements OnInit {

  public isAuthenticated$?: Observable<boolean>;
  public userInfo$: Observable<UserInfoDto>;

  constructor(private moviesClient: MovieClient,
    private authClient: AuthClient,
    private authorizeService: AuthorizeService) { }

  movies$ = this.moviesClient.get()
    .pipe(
      tap(movies => console.log(movies)),
      catchError(err => {
        console.log(err);
        return EMPTY;
      })
    );
    
  ngOnInit(): void {
    this.isAuthenticated$ = this.authorizeService.isAuthenticated();
    this.authorizeService.getUser()
      .subscribe({
        next: (response) => {
          console.log('get user:', response);
          this.userInfo$ = this.authClient.get(response.sub);
        },
        error: (e) => console.error(e),
        complete: () => console.log('completed')
      });
    
  }

  deleteMovie(id: number) {
      this.moviesClient.delete(id)
          .subscribe({
            next: (response) => {
              alert("Movie deleted successfully"); 
              this.movies$ = this.moviesClient.get();
            },
            error: (e) => console.error(e),
            complete: () => console.log('completed')
          })
  }

}
