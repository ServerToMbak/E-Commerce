import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { IBrand } from '../shared/models/brands';
import { IPagination } from '../shared/models/pagination';
import { IType } from '../shared/models/productType';
import { shopParams } from '../shared/models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/'

  constructor(private htttp: HttpClient) { }

  getProducts(shopParams:shopParams){
    let params = new HttpParams();

    if(shopParams.brandId !== 0){
      params = params.append('brandId',shopParams.brandId.toString())

    }
    if(shopParams.typeId !== 0){
      params = params.append('typeId',shopParams.typeId.toString());
    }

    if(shopParams.search){
      params = params.append("search",shopParams.search);
    }
      params = params.append("sort", shopParams.sort);
      params = params.append("pageIndex", shopParams.pageNumber.toString());
      params = params.append("pageIndex", shopParams.pageSize.toString());

    return this.htttp.get<IPagination>(this.baseUrl + 'products', {observe: 'response', params})
    .pipe(
      map(response => {
        return response.body;
      })

    );
  }

  getBrands(){
    return this.htttp.get<IBrand[]>(this.baseUrl + 'products/brands');
  }

  getTypes(){
    return this.htttp.get<IType[]>(this.baseUrl + 'products/types');
  }
}