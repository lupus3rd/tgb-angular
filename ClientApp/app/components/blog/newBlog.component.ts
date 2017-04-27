import { Component } from '@angular/core';
import { BlogServices } from '../../services/services';
import { Response } from '@angular/http';
import { FormGroup, FormControl, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';


@Component({
    selector: 'new-Blog',
    templateUrl: './newBlog.component.html',

})
export class newBlogComponent {
    public CategoryList = [];
    public formData: FormGroup;
    public constructor(private empService: BlogServices) {
        this.empService.getCategoryList()
            .subscribe(
            (data: Response) => (this.CategoryList = data.json())
            );

        this.formData = new FormGroup({
            'BlogName': new FormControl('', [Validators.required]),
            'Content': new FormControl('', Validators.required),
            'Category': new FormControl(0, [Validators.required, this.customValidator])
        });

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
                Title: this.formData.value.BlogName,
                CategoryId: this.formData.value.Category,
                Content: this.formData.value.Content
            };
            this.empService.postData(Obj).subscribe();
            alert("Blog Inserted Successfully");
        }

    }


}  