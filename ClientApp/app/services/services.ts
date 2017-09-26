import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { Location, LocationStrategy, PathLocationStrategy } from '@angular/common';

@Injectable()
export class BlogServices {
    private link: string;
    //Whats a promise?

    constructor(private http: Http, private locationStrategy: LocationStrategy) {
        this.link = this.locationStrategy.getBaseHref(); //not sure if this is the best solution
    }

    getBlogList() {
        return this.http.get(this.link+'api/Blog');
    }

    getCategoryList() {
        return this.http.get(this.link +'api/categories');
    }  

    getBlogDetails(empId: any) {
        return this.http.get(this.link +'api/Blog/' + empId);
    }  

    postData(empObj: any) {
        let headers = new Headers({
            'Content-Type':
            'application/json; charset=utf-8'
        });
        let options = new RequestOptions({ headers: headers });
        return this.http.post(this.link +'api/Blog', JSON.stringify(empObj), options);
    }  

    editBlogData(empObj: any) {
        let headers = new Headers({
            'Content-Type':
            'application/json; charset=utf-8'
        });
        let options = new RequestOptions({ headers: headers });
        return this.http.put(this.link +'Blog', JSON.stringify(empObj), options);
    }  

    removeBlogDetails(empId: any) {
        let headers = new Headers({
            'Content-Type':
            'application/json; charset=utf-8'
        });
        return this.http.delete(this.link +'Blog', new RequestOptions({
            headers: headers,
            body: empId
        }));
    }  

}   