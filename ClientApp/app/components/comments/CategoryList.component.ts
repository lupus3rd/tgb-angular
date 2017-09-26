import { Component } from '@angular/core';
import { BlogServices } from '../../services/services';
import { Response } from '@angular/http';
import { Router, ActivatedRoute, Params } from '@angular/router';

@Component({
    selector: 'category-list',
    templateUrl: './categoryList.component.html'
})
export class CategoryListComponent {
    public CategoryList = [];

    public constructor(private blService: BlogServices) {
        this.blService.getCategoryList()
            .subscribe(
            (data: Response) => (this.CategoryList = data.json())
            );
        
    }
}  