import { Component } from '@angular/core';
import { BlogServices } from '../../services/services';
import { Response } from '@angular/http';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
    selector: 'edit-Blog',
    templateUrl: './editBlog.component.html'
})
export class editBlogComponent {
    private blogId: number;
    public BlogDetails = {};
    public BlogName: string;
    public CategoryList = [];
    public formData: FormGroup;

    public constructor(private blogService: BlogServices, private activatedRoute: ActivatedRoute) {
        this.activatedRoute.params.subscribe((params: Params) => {
            this.blogId = params['id'];
        });

        this.blogService.getCategoryList()
            .subscribe(
            (data: Response) => (this.CategoryList = data.json())
            );

        this.formData = new FormGroup({
            'BlogId': new FormControl('', [Validators.required]),
            'BlogName': new FormControl('', [Validators.required]),
            'Content': new FormControl('', Validators.required),
            'Category': new FormControl(0, [Validators.required, this.customValidator])
        });

        this.blogService.getBlogDetails(this.blogId)
            .subscribe((data: Response) => (
                this.formData.patchValue({ BlogId: data.json().id }),
                this.formData.patchValue({ BlogName: data.json().title }),
                this.formData.patchValue({ Content: data.json().content }),
                this.formData.patchValue({ Category: data.json().categoryId })

            ));
 
    }

    customValidator(control: FormControl): { [s: string]: boolean } {
        if (control.value == "0") {
            return { data: true };
        }
        else {
            return null;
        }
    }

    submitData() {
        if (this.formData.valid) {
            var Obj = {
                Id: this.formData.value.BlogId,
                Title: this.formData.value.BlogName,
                CategoryId: this.formData.value.CategoryId,
                Content: this.formData.value.Content
            };
            this.blogService.editBlogData(Obj)
                .subscribe((data: Response) => (alert("Blog Updated Successfully")));;

        }

    }
}  