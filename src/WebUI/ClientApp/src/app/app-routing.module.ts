import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthorizeGuard } from '../api-authorization/authorize.guard';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { TodoComponent } from './todo/todo.component';
import { TokenComponent } from './token/token.component';
import { MovieComponent } from './movie/movie.component';
import { ManageMovieComponent } from './movie/manage-movie/manage-movie.component';
import { TheatreComponent } from './theatre/theatre.component';
import { ManageTheatreComponent } from './theatre/manage-theatre/manage-theatre.component';
import { BookingComponent } from './booking/booking.component';

export const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'movie', component: MovieComponent },
  { path: 'movie/manage', component: ManageMovieComponent, canActivate: [AuthorizeGuard] },
  { path: 'movie/manage/:id', component: ManageMovieComponent, canActivate: [AuthorizeGuard] },
  { path: 'theatre', component: TheatreComponent },
  { path: 'theatre/manage', component: ManageTheatreComponent, canActivate: [AuthorizeGuard] },
  { path: 'theatre/manage/:id', component: ManageTheatreComponent, canActivate: [AuthorizeGuard] },
  { path: 'booking', component: BookingComponent },
  { path: 'token', component: TokenComponent, canActivate: [AuthorizeGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
