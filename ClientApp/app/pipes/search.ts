import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'searchFilter'
})

export class filterSearch implements PipeTransform {
    transform(value: any, args: string): any {
        if (args == null || args == undefined) {
            return value;
        }
        else {
            let filter = args.toLocaleLowerCase();
            return filter ? value.filter(blog => (blog.title.toLocaleLowerCase().indexOf(filter) != -1)
                || (blog.category.toLocaleLowerCase().indexOf(filter) != -1)
                || (blog.content.toLocaleLowerCase().indexOf(filter) != -1)
            ) : value;
        }
    }
}  
