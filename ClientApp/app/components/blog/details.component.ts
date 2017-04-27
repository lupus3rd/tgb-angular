import { Component } from '@angular/core';
import { BlogServices } from '../../services/services';
import { Response } from '@angular/http';
import { Router, ActivatedRoute, Params } from '@angular/router';

@Component({
    selector: 'blog-detail',
    templateUrl: './details.component.html'
})
export class DetailsComponent {
    private BlogId: number;
    public BlogDetails = {};

    public constructor(private empService: BlogServices, private activatedRoute: ActivatedRoute) {
        this.activatedRoute.params.subscribe((params: Params) => {
            this.BlogId = params['id'];
        });

        this.empService.getBlogDetails(this.BlogId)
            .subscribe((data: Response) => (this.BlogDetails["Title"] = data.json().title,
                this.BlogDetails["CreatedOn"] = data.json().createdOn,
                this.BlogDetails["Content"] = data.json().content,
                this.BlogDetails["Category"] = data.json().name,
                this.BlogDetails["Comments"] = data.json().comments

            ));
        
    }
}  