import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { APP_BASE_HREF } from '@angular/common';

import { TestBed, async } from '@angular/core/testing';

import { HomeComponent } from './home.component';

import { CookieService } from 'ngx-cookie-service';

describe('HomeComponent', () => {
  const routes: Routes = [
    { path: 'HomeComponent', component: HomeComponent }
  ];
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot(routes)
      ],
      declarations: [HomeComponent],
      providers: [
        CookieService,
        { provide: APP_BASE_HREF, useValue: '/' }
      ]
    }).compileComponents();
  }));

  it('should create the HomeComponent', async(() => {
    const fixture = TestBed.createComponent(HomeComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  }));
});
