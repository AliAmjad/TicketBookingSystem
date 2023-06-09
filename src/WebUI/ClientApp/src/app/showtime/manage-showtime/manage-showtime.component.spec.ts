import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowtimeComponent } from './manage-showtime.component';

describe('ShowtimeComponent', () => {
  let component: ShowtimeComponent;
  let fixture: ComponentFixture<ShowtimeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShowtimeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShowtimeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
