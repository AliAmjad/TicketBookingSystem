import { Component, OnInit } from '@angular/core';
import { catchError, EMPTY, Observable, tap } from 'rxjs';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { AuthClient, ShowTimeClient, UserInfoDto } from '../web-api-client';

@Component({
  selector: 'app-showtime',
  templateUrl: './showtime.component.html',
  styleUrls: ['./showtime.component.scss']
})
export class ShowtimeComponent implements OnInit {

  public isAuthenticated$?: Observable<boolean>;
  public userInfo$: Observable<UserInfoDto>;

  constructor(private showtimeClient: ShowTimeClient,
    private authClient: AuthClient,
    private authorizeService: AuthorizeService) { }

  showtimes$ = this.showtimeClient.get()
    .pipe(
      tap(showtimes => console.log(showtimes)),
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
          this.userInfo$ = this.authClient.get(response.sub);
        },
        error: (e) => console.error(e),
        complete: () => console.log('completed')
      });
    
  }

  deleteShowTime(id: number) {
      // this.showtimeClient.delete(id)
      //     .subscribe({
      //       next: (response) => {
      //         alert("Show time deleted successfully"); 
      //         // this.showtimes$ = this.moviesClient.get();
      //       },
      //       error: (e) => console.error(e),
      //       complete: () => console.log('completed')
      //     })
  }
}
