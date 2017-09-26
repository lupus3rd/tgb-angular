import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UniversalModule } from 'angular2-universal';
import { AppComponent } from './components/app/app.component'
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { DetailsComponent } from './components/blog/details.component';
import { newBlogComponent } from './components/blog/newBlog.component';
import { editBlogComponent } from './components/blog/editBlog.component';
import { CategoryListComponent } from './components/blogCategory/CategoryList.component';
import { manageCommentsComponent } from './components/comments/manageComments.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BlogServices } from './services/services';
import { filterSearch } from './pipes/search';
import { filterCategorySearch } from './pipes/categorySearch';

@NgModule({
    bootstrap: [AppComponent],
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        DetailsComponent,
        newBlogComponent,
        editBlogComponent,
        manageCommentsComponent,
        CategoryListComponent,
        filterSearch,
        filterCategorySearch
    ],
    imports: [
        FormsModule,
        ReactiveFormsModule,
        UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.  
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'details/:id', component: DetailsComponent },
            { path: 'new', component: newBlogComponent },
            { path: 'edit/:id', component: editBlogComponent },
            { path: 'categoryList', component: CategoryListComponent },
            { path: 'comment/:id', component: manageCommentsComponent },
          { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [BlogServices]
})
export class AppModule {
}   