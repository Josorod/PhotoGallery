import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { FooterComponent } from './footer/footer.component';
import { ReactiveFormsModule } from '@angular/forms'; 
import { UserLoginComponent } from './user/user-login/user-login.component';
import { UserRegisterComponent } from './user/user-register/user-register.component';
import { provideHttpClient, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AlbumComponent } from './album/album.component';
//import { JwtInterceptor } from './helpers/jwt.interceptor'; 

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule,
            RouterOutlet,
            NavBarComponent,
            FooterComponent,
            AlbumComponent,
            UserLoginComponent,
            UserRegisterComponent,
            RouterLink,
            RouterLinkActive,
            ReactiveFormsModule],
  // providers: [
  //   {
  //     provide: HTTP_INTERCEPTORS,
  //     useClass: JwtInterceptor,
  //     multi: true
  //   }
  // ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'ClientApp';
}
