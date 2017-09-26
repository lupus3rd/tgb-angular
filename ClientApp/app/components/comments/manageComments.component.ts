import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl, ValidatorFn, FormArray } from '@angular/forms';

import 'rxjs/add/operator/debounceTime';
import { BlogServices } from '../../services/services';
import { Response } from '@angular/http';
import { Router, ActivatedRoute, Params } from '@angular/router';

@Component({
    selector: 'manage-comments',
    templateUrl: './manageComments.component.html'
})
export class manageCommentsComponent implements OnInit  {
    commentForm: FormGroup;
    Comment: Comment = new Comment();

    constructor(private fb: FormBuilder) { }

    ngOnInit(): void {
        this.commentForm = this.fb.group({
            Content: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]]
        });

    }


    //submitData() {
    //    if (this.formData.valid) {
    //        var Obj = {
    //            Id: this.formData.value.categoryId,
    //            Title: this.formData.value.CategoryName,
    //        };
    //        this.blogService.editBlogData(Obj)
    //            .subscribe((data: Response) => (alert("Blog Updated Successfully")));;

    //    }

  //  }
}  