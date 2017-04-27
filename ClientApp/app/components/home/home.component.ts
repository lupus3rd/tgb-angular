import { Component } from '@angular/core';
import { BlogServices } from '../../services/services';
import { Response } from '@angular/http';
@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {
    public BlogList = [];
    public constructor(private blService: BlogServices) {
        this.blService.getBlogList()
            .subscribe(
            (data: Response) => (this.BlogList = data.json())
            );

    }

    deleteBlog(blId: number) {
        var status = confirm("Are You want to delete this Blog ?");
        if (status == true) {
            this.blService.removeBlogDetails(blId)
                .subscribe((data: Response) => (alert("Blog Deleted Successfully")));

            //Get new list of Blog  
            this.blService.getBlogList()
                .subscribe(
                (data: Response) => (this.BlogList = data.json())
                );
        }

    }  
} 