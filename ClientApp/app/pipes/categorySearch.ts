import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'categorySearchFilter'
})

export class filterCategorySearch implements PipeTransform {
    transform(value: any, args: string): any {
        if (args == null || args == undefined) {
            return value;
        }
        else {
            let filter = args.toLocaleLowerCase();
            return filter ? value.filter(category => (category.name.toLocaleLowerCase().indexOf(filter) != -1)
              
            ) : value;
        }
    }
}  