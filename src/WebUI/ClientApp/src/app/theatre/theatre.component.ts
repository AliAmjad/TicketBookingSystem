import { Component, OnInit } from '@angular/core';
import { catchError, EMPTY, forkJoin, Observable, tap } from 'rxjs';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { AuthClient, TheatreClient, UserInfoDto } from '../web-api-client';

@Component({
  selector: 'app-theatre',
  templateUrl: './theatre.component.html',
  styleUrls: ['./theatre.component.scss']
})
export class TheatreComponent implements OnInit {

  public isAuthenticated$?: Observable<boolean>;
  public userInfo$: Observable<UserInfoDto>;

  constructor(private theatreClient: TheatreClient,
    private authClient: AuthClient,
    private authorizeService: AuthorizeService) { }

  theatres$ = this.theatreClient.get()
    .pipe(
      tap(t => console.log(t)),
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

  deleteTheatre(id: number) {
      this.theatreClient.delete(id)
          .subscribe({
            next: (response) => {
              alert("Theatre deleted successfully"); 
              this.theatres$ = this.theatreClient.get();
            },
            error: (e) => console.error(e),
            complete: () => console.log('completed')
          })
  }

}
