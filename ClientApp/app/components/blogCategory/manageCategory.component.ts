import { Component } from '@angular/core';
import { BlogServices } from '../../services/services';
import { Response } from '@angular/http';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
    selector: 'manage-category',
    templateUrl: './manageCategory.component.html'
})
export class manageCategoryComponent {
    private categoryId: number;
    public CategoryName: string;
    public formData: FormGroup;

    public constructor(private blogService: BlogServices, private activatedRoute: ActivatedRoute) {
        this.activatedRoute.params.subscribe((params: Params) => {
            this.categoryId = params['id'];
        });


        this.formData = new FormGroup({
            'categoryId': new FormControl(''),
            'CategoryName': new FormControl('', [Validators.required]),
    
        });

    }

    submitData() {
        if (this.formData.valid) {
            var Obj = {
                Id: this.formData.value.categoryId,
                Title: this.formData.value.CategoryName,
            };
            this.blogService.editBlogData(Obj)
                .subscribe((data: Response) => (alert("Blog Updated Successfully")));;

        }

    }
}  