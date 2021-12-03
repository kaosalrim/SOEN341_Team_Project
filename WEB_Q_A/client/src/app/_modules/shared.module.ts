import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { HttpClientModule } from '@angular/common/http';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { ModalModule } from 'ngx-bootstrap/modal';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
    }),
    TabsModule.forRoot(),
    PaginationModule.forRoot(),
    HttpClientModule,
    AngularEditorModule,
    ModalModule.forRoot()
  ],
  exports: [
    BsDropdownModule,
    ToastrModule,
    TabsModule,
    PaginationModule,
    HttpClientModule,
    AngularEditorModule,
    ModalModule
  ],
})
export class SharedModule {}
