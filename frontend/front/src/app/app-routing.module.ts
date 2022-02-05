import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CryptoComponent } from './crypto/crypto.component';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'crypto', component: CryptoComponent },
  { path: '', redirectTo: 'login', pathMatch: 'full' },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
