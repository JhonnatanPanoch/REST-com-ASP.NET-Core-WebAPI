import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { Router } from '@angular/router';

import { Observable } from 'rxjs';

import { Product } from '../models/Product';
import { Fornecedor } from '../models/Fornecedor';
import { ProdutoService } from '../services/produtoService';

@Component({
  selector: 'app-cadastro',
  templateUrl: './cadastro.component.html'
})
export class CadastroComponent implements OnInit {

  produtoForm: FormGroup;
  produto: Product;
  errors: any[] = [];
  fornecedores: Fornecedor[];
  imagemForm: any;
  imagemNome: string;
  imageBase64: any;

  constructor(private fb: FormBuilder,
              private router: Router,
              private produtoService: ProdutoService) {

    this.produtoService.obterFornecedores()
      .subscribe(
        fornecedores => this.fornecedores = fornecedores, 
        fail => this.errors = fail.error.errors
      );

    this.imagemForm = new FormData();
  }

  ngOnInit(): void {

    this.produtoForm = this.fb.group({
      supplierId: '',
      name: '',
      description: '',
      imageUpload: '',
      image: '',
      value: '0',
      active: new FormControl(false),
      supplierName: ''
    });
  }

  cadastrarProduto() {
    if (this.produtoForm.valid && this.produtoForm.dirty) {

      let produtoForm = Object.assign({}, this.produto, this.produtoForm.value);
      produtoForm.active = this.produtoForm.get('active').value

      this.produtoHandle(produtoForm)
        .subscribe(
          result => { this.onSaveComplete(result) },
          fail => { this.onError(fail) }
        );
    }
  }

  onSaveComplete(response: any) {
    this.router.navigate(['/lista-produtos']);
  }

  onError(fail: any) {
    this.errors = fail.error.errors;
  }

  produtoHandleAlternativo(produto: Product): Observable<Product> {

    let formdata = new FormData();
    produto.image = this.imagemNome;
    produto.imageUpload = null;

    formdata.append('produto', JSON.stringify(produto));
    formdata.append('ImagemUpload', this.imagemForm, this.imagemNome);

    return this.produtoService.registrarProdutoAlternativo(formdata);
  }

  produtoHandle(produto: Product): Observable<Product> {

    produto.imageUpload = this.imageBase64;
    produto.image = this.imagemNome;

    return this.produtoService.registrarProduto(produto);
  }

  upload(file: any) {
    // necessario para upload via IformFile
    this.imagemForm = file[0];
    this.imagemNome = file[0].name;

    // necessario para upload via base64
    var reader = new FileReader();
    reader.onload = this.manipularReader.bind(this);
    reader.readAsBinaryString(file[0]);
  }

  manipularReader(readerEvt: any) {
    var binaryString = readerEvt.target.result;
    this.imageBase64 = btoa(binaryString);
  } 
}


